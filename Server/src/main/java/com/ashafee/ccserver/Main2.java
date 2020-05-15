package com.ashafee.ccserver;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class Main2 {
    public static void main(String[] args) throws Exception {
        //runProcess("java -version");
        exec("testabc", new ArrayList<String>(), new ArrayList<String>());
    }

    //from https://dzone.com/articles/running-a-java-class-as-a-subprocess
    public static int exec(String className, List<String> jvmArgs, List<String> args) throws Exception {
        String javaHome = System.getProperty("java.home");
        String test = new File(".").getCanonicalPath();
        String javaBin = javaHome + File.separator + "bin" + File.separator + "java";
        String classpath = test + ";" + System.getProperty("java.class.path");
        List<String> command = new ArrayList<>();
        command.add(javaBin);
        command.addAll(jvmArgs);
        command.add("-cp");
        command.add(classpath);
        //className = test + File.separator + className + ".class";
        command.add(className);
        command.addAll(args);
        ProcessBuilder builder = new ProcessBuilder(command)
                .directory(null)
                .redirectErrorStream(true);
        Process process = builder
                .start();
        process.getOutputStream().close();

        printLines(command + " stdout:", process.getInputStream());
        printLines(command + " stderr:", process.getErrorStream());

        process.waitFor();
        return process.exitValue();
    }

    // From Almas Baimagambetov
    private static void runProcess(String command) throws Exception {
        ProcessBuilder builder = new ProcessBuilder(command.split(" "));
        Process pro = builder.directory(null).redirectErrorStream(true).start();
        pro.getOutputStream().close();
        printLines(command + " stdout:", pro.getInputStream());
        printLines(command + " stderr:", pro.getErrorStream());
        pro.waitFor();
        pro.getInputStream().close();
        pro.getErrorStream().close();
    }

    private static void printLines(String name, InputStream ins) throws Exception {
        Thread t = new Thread(() -> {
            String line = null;
            try (BufferedReader in = new BufferedReader(new InputStreamReader(ins))) {
                while ((line = in.readLine()) != null) {
                    System.out.println(line);
//                    if (line.contains("-version=")) {
//                        currentVersion = line.replace("-version=", "");
//                    }
                }
            }
            catch (Exception e) {
//                Debug.trace(1, "Cannot open stream to Charon.jar: " + e.getMessage());
            }
        });
        t.start();
    }
}
