pipeline {
    agent any
    triggers {
        pollSCM('1 * * * *')
        //cron('10 * * * *')
    }
    stages {
        stage('Setup') {
            steps {
                dir("Backend/Test"){
                    sh "rm -rf TestResults"
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
                    sh "ls -R Test/TestResults"
                    archiveArtifacts "Test/TestResults/*/coverage.cobertura.xml"
                    publishCoverage adapters: [istanbulCoberturaAdapter(path: "Test/TestResults/*/coverage.cobertura.xml", thresholds:
                    [[failUnhealthy: true, thresholdTarget: 'Conditional', unhealthyThreshold: 80.0, unstableThreshold: 50.0]])], checksName: '',
                    sourceFileResolver: sourceFiles('NEVER_STORE')
                }
            }
        }
        
    }
}