pipeline {
    agent any
    triggers {
        pollSCM('H * * * *')
        //cron('10 * * * *')
    }
    stages {
        stage('Setup') {
            steps {
                dir("Test"){
                    //sh "rm -rf TestResults"
                    echo "setup stage"
                }
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet restore WebAPI/WebAPI.csproj'
                sh 'dotnet build WebAPI/WebAPI.csproj'
                echo 'build complete'
            }
        }
        stage('Test') {
            steps {
                dir("Test"){
                    sh "dotnet add package coverlet.collector"
                    sh "dotnet test --collect:'XPlat Code Coverage'"
                }
            }
            post {
                success{
                    archiveArtifacts "Test/TestResults/*/coverage.cobertura.xml"
                    publishCoverage adapters: [istanbulCoberturaAdapter(path: "Test/TestResults/*/coverage.cobertura.xml", thresholds:
                    [[failUnhealthy: true, thresholdTarget: 'Conditional', unhealthyThreshold: 30.0, unstableThreshold: 20.0]])], checksName: '',
                    sourceFileResolver: sourceFiles('NEVER_STORE')
                }
            }
        }
        stage('Deploy') {
            steps {
                sh "docker build -t 'jobman-api:testbuild' ."
                // sh "docker-compose -f /home/jenkins/docker-jobmanagement/jobmanagementapi/docker-compose.yml up -d"
                echo "deployment do work? no, but docker buld do"
        }
        stage('PerformanceTest'){
            steps {
                sh "k6 run script.js --vus 51 --duration 10s"
               
            }
        }
    }
}