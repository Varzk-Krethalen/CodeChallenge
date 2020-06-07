package com.ashafee.ccserver.challenge.java;

import com.ashafee.ccserver.challenge.ChallengeTest;
import org.springframework.util.FileSystemUtils;

import javax.tools.*;
import javax.tools.JavaCompiler.CompilationTask;
import java.io.*;
import java.net.URI;
import java.nio.file.FileSystem;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.*;


public class JavaCompiler implements AutoCloseable {
    private StringBuilder output = new StringBuilder();
    private final Path baseChallengeDir;
    private String challengeDir;
    private final String className;
    private List<String> jvmArgs;

    public JavaCompiler(String challengeDir, String className) {
        baseChallengeDir = Path.of(challengeDir);
        this.className = className;
        jvmArgs = new ArrayList<String>();
        if (Files.notExists(baseChallengeDir)) {
            try {
                Files.createDirectories(baseChallengeDir);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    public String getLastOutput() {
        return output.toString();
    }

    // adapted from https://stackoverflow.com/questions/21544446/how-do-you-dynamically-compile-and-load-external-java-classes
    public Boolean compile(String code) {
        output = new StringBuilder();
        try {
            challengeDir = baseChallengeDir + "/" + Files.createTempDirectory(baseChallengeDir, null).getFileName();

            /** Compilation Requirements *********************************************************************************************/
            DiagnosticCollector<JavaFileObject> diagnostics = new DiagnosticCollector<JavaFileObject>();
            javax.tools.JavaCompiler compiler = ToolProvider.getSystemJavaCompiler();
            StandardJavaFileManager fileManager = compiler.getStandardFileManager(diagnostics, null, null);
            fileManager.setLocation(StandardLocation.CLASS_OUTPUT, Arrays.asList(new File(challengeDir)));
            //TODO: create dir if not there
            CompilationTask compilationTask = compiler.getTask(
                    null,
                    fileManager,
                    diagnostics,
                    new ArrayList<String>(),
                    null,
                    Arrays.asList(new JavaSourceFromString(className, code)));
            /********************************************************************************************* Compilation Requirements **/
            if (compilationTask.call()) {
                return true;
            } else {
                for (Diagnostic<? extends JavaFileObject> diagnostic : diagnostics.getDiagnostics()) {
                    String verboseErrorMsg =  diagnostic.toString();
                    output.append(String.format("Error on line %d: %s\n",
                            diagnostic.getLineNumber(),
                            verboseErrorMsg.substring(verboseErrorMsg.indexOf(':') + 3)));
                }
            }
            fileManager.close();
        } catch (IOException e) {
            output.append(e.getMessage());
        }
        return false;
    }

    public Boolean runTests(Set<ChallengeTest> tests) {
        for (ChallengeTest test : tests) {
            runTest(test);
            if (!output.toString().equals(test.getExpectedOutput())) {
                String inputs = test.getInputArgs().length() > 0 ? String.format("Input: %s, ", test.getInputArgs()) : "";
                String result = inputs + "Expected: " + test.getExpectedOutput() + ", Got: " + output.toString();
                output.replace(0, output.length(), result);
                return false;
            }
        }
        return true;
    }

    private void runTest(ChallengeTest test) {
        List<String> arguments = Arrays.asList(test.getInputArgs().split(","));
        output.setLength(0);
        try {
            executeCode(className, arguments);
        } catch (Exception e) {
            output.append(e.getMessage());
        }
    }

    //adapted from https://dzone.com/articles/running-a-java-class-as-a-subprocess
    private int executeCode(String className, List<String> args) throws Exception {
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
        Process process = (new ProcessBuilder(command))
                .redirectErrorStream(true)
                .start();
        process.getOutputStream().close();

        setStreamOutput(process.getInputStream(), output);
        setStreamOutput(process.getErrorStream(), output);
        return process.waitFor();
    }

    /**
     * print lines out from an InputStream to a StringBuilder
     * adapted from code by Almas Baimagambetov
     */
    private void setStreamOutput(InputStream ins, StringBuilder stringBuilder) {
        Thread t = new Thread(() -> {
            String line = null;
            try (BufferedReader in = new BufferedReader(new InputStreamReader(ins))) {
                while ((line = in.readLine()) != null) {
                    stringBuilder.append(line);
                }
            }
            catch (Exception e) { //TODO: do something with this
//                Debug.trace(1, "Cannot open stream to Charon.jar: " + e.getMessage());
            }
        });
        t.start();
    }

    @Override
    public void close() {
        try { //remove temp directory used
            if (Files.exists(Path.of(challengeDir))){
                FileSystemUtils.deleteRecursively(Path.of(challengeDir));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * A file object used to represent source coming from a string.
     * Taken from https://docs.oracle.com/javase/7/docs/api/javax/tools/JavaCompiler.html
     */
    private class JavaSourceFromString extends SimpleJavaFileObject {
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
