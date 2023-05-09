pipeline {
    agent any
    triggers {
        pollSCM('1 * * * *')
        //cron('10 * * * *')
    }
    stages {
        stage('Setup') {
            steps {
                dir("Test"){
                    sh "rm -rf TestResults"
                }
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet restore WebAPI/API.csproj'
                sh 'dotnet build WebAPI/API.csproj'
                echo 'build complete'
            }
        }
        stage('Test') {
            steps {
                dir("Test"){
                    //sh "dotnet add package coverlet.collector"
                    sh "dotnet test"
                }
            }
        }
        stage('Deploy') {
            steps {
                // dotnet publish -o ../../outputs
                echo 'deploy'
            }
        }
    }
}