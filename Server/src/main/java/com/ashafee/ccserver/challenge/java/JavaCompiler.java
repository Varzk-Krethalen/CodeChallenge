package com.ashafee.ccserver.challenge.java;

import org.springframework.util.FileSystemUtils;

import javax.tools.*;
import java.io.*;
import java.net.URI;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;


public class JavaCompiler {
    private StringBuilder output = new StringBuilder();
    private final Path baseChallengeDir;
    private String challengeDir;
    private final String className;

    public JavaCompiler(String challengeDir, String className) {
        baseChallengeDir = Path.of(challengeDir);
        this.className = className;
        if (Files.notExists(baseChallengeDir)) {
            try {
                Files.createDirectories(baseChallengeDir);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    // adapted from https://stackoverflow.com/questions/21544446/how-do-you-dynamically-compile-and-load-external-java-classes
    public String compile(String code) {
        try {
            challengeDir = baseChallengeDir + "/" + Files.createTempDirectory(baseChallengeDir, null).getFileName();

            /** Compilation Requirements *********************************************************************************************/
            DiagnosticCollector<JavaFileObject> diagnostics = new DiagnosticCollector<JavaFileObject>();
            javax.tools.JavaCompiler compiler = ToolProvider.getSystemJavaCompiler();
            StandardJavaFileManager fileManager = compiler.getStandardFileManager(diagnostics, null, null);
            fileManager.setLocation(StandardLocation.CLASS_OUTPUT, Arrays.asList(new File(challengeDir)));
            //TODO: create dir if not there
            Writer outputWriter = new StringWriter();

            javax.tools.JavaCompiler.CompilationTask task = compiler.getTask(
                    null,
                    fileManager,
                    diagnostics,
                    new ArrayList<String>(),
                    null,
                    Arrays.asList(new JavaSourceFromString(className, code)));
            /********************************************************************************************* Compilation Requirements **/
            if (task.call()) {
                List<String> arguments = new ArrayList<>();

                try {
                    exec(className, new ArrayList<String>(), arguments);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }

                /************************************************************************************************* Load and execute **/
            } else {
                for (Diagnostic<? extends JavaFileObject> diagnostic : diagnostics.getDiagnostics()) {
                    System.out.format("Error on line %d in %s%n",
                            diagnostic.getLineNumber(),
                            diagnostic.getSource().toUri());
                }
            }
            fileManager.close();
            return outputWriter.toString();
        } catch (IOException e) {
            return e.getMessage();
        }
        finally { //TODO: if compile/run are split out, consider when to delete the temp dir
            try {
                FileSystemUtils.deleteRecursively(Path.of(challengeDir));
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    //TODO: private?
    //adapted from https://dzone.com/articles/running-a-java-class-as-a-subprocess
    public int exec(String className, List<String> jvmArgs, List<String> args) throws Exception {
        String javaHome = System.getProperty("java.home");
        String javaBin = javaHome + File.separator + "bin" + File.separator + "java";
        String classpath = new File(".").getCanonicalPath() + "/" + challengeDir + ";" + System.getProperty("java.class.path");
        List<String> command = new ArrayList<>();
        command.add(javaBin);
        command.addAll(jvmArgs); //TODO: File access restriction
        command.add("-cp");
        command.add(classpath);
        command.add(className);
        command.addAll(args);
        ProcessBuilder builder = new ProcessBuilder(command);
        Process process = builder
                .redirectErrorStream(true)
                .start();
        process.getOutputStream().close();

        printLines(command + " stdout:", process.getInputStream());
        printLines(command + " stderr:", process.getErrorStream());

        process.waitFor();
        return process.exitValue();
    }

    /**
     * print lines out from an InputStream to a stringbuilder
     * adapted from code by Almas Baimagambetov
     */
    private void printLines(String name, InputStream ins) {
        output = new StringBuilder();
        Thread t = new Thread(() -> {
            String line = null;
            try (BufferedReader in = new BufferedReader(new InputStreamReader(ins))) {
                while ((line = in.readLine()) != null) {
                    output.append(line);
                }
            }
            catch (Exception e) { //TODO: do something with this
//                Debug.trace(1, "Cannot open stream to Charon.jar: " + e.getMessage());
            }
        });
        t.start();
    }

    public String getLastOutput()
    {
        return output.toString();
    }

    //TODO: private?
    /**
     * A file object used to represent source coming from a string.
     * Taken from https://docs.oracle.com/javase/7/docs/api/javax/tools/JavaCompiler.html
     */
    public class JavaSourceFromString extends SimpleJavaFileObject {
        /**
         * The source code of this "file".
         */
        final String code;

        /**
         * Constructs a new JavaSourceFromString.
         * @param name the name of the compilation unit represented by this file object
         * @param code the source code for the compilation unit represented by this file object
         */
        JavaSourceFromString(String name, String code) {
            super(URI.create("string:///" + name.replace('.','/') + Kind.SOURCE.extension),
                    Kind.SOURCE);
            this.code = code;
        }

        @Override
        public CharSequence getCharContent(boolean ignoreEncodingErrors) {
            return code;
        }
    }
}
