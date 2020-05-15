package com.ashafee.ccserver.challenge.java;

import javax.tools.*;
import java.io.*;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.URI;
import java.net.URL;
import java.net.URLClassLoader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

// adapted from https://stackoverflow.com/questions/21544446/how-do-you-dynamically-compile-and-load-external-java-classes
public class Compiler {
    private StringBuilder output = new StringBuilder();

    public String compile(String code) {
        try {
            /** Compilation Requirements *********************************************************************************************/
            DiagnosticCollector<JavaFileObject> diagnostics = new DiagnosticCollector<JavaFileObject>();
            JavaCompiler compiler = ToolProvider.getSystemJavaCompiler();
            StandardJavaFileManager fileManager = compiler.getStandardFileManager(diagnostics, null, null);

            Writer outputWriter = new StringWriter();

            JavaCompiler.CompilationTask task = compiler.getTask(
                    null,
                    fileManager,
                    diagnostics,
                    new ArrayList<String>(),
                    null,
                    Arrays.asList(new JavaSourceFromString("Challenge", code)));
            /********************************************************************************************* Compilation Requirements **/
            if (task.call()) {
                List<String> arguments = new ArrayList<>();

                try {
                    exec("Challenge", new ArrayList<String>(), arguments);
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
        } catch (IOException exp) { //| ClassNotFoundException | InstantiationException | IllegalAccessException | NoSuchMethodException | InvocationTargetException exp) {
            return exp.getMessage();
        }
    }

    //from https://dzone.com/articles/running-a-java-class-as-a-subprocess
    public int exec(String className, List<String> jvmArgs, List<String> args) throws Exception {
        String javaHome = System.getProperty("java.home");
        String javaBin = javaHome + File.separator + "bin" + File.separator + "java";
        String classpath = new File(".").getCanonicalPath() + ";" + System.getProperty("java.class.path");
        List<String> command = new ArrayList<>();
        command.add(javaBin);
        command.addAll(jvmArgs);
        command.add("-cp");
        command.add(classpath);
        command.add(className);
        command.addAll(args);
        File test = new File(".");
        ProcessBuilder builder = new ProcessBuilder(command)
                .directory(test)
                .redirectErrorStream(true);
        Process process = builder
                .start();
        process.getOutputStream().close();

        printLines(command + " stdout:", process.getInputStream());
        printLines(command + " stderr:", process.getErrorStream());

        process.waitFor();
        return process.exitValue();
    }

    private void printLines(String name, InputStream ins) throws Exception {
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
        return  output.toString();
    }

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
