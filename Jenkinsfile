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
                    [[failUnhealthy: true, thresholdTarget: 'Conditional', unhealthyThreshold: 80.0, unstableThreshold: 50.0]])], checksName: '',
                    sourceFileResolver: sourceFiles('NEVER_STORE')
                }
            }
        }
        stage('Deploy') {
            steps {
                sh "docker build -t 'jobman-api:testbuild' ."
                sh "docker-compose -f /home/jenkins/docker-jobmanagement/jobmanagementapi/docker-compose.yml up -d"
            }
        }
        
    }
}