pipeline {
    agent { label 'winserver' }

    options {
        // Налаштування зберігання:
        // daysToKeepStr: скільки днів зберігати білд (наприклад, 7 днів)
        // numToKeepStr: яку кількість останніх білдів зберігати (наприклад, останні 10)
        // artifactDaysToKeepStr: окремо для артефактів (якщо треба видаляти їх раніше за логи)
        buildDiscarder(logRotator(
            daysToKeepStr: '7', 
            numToKeepStr: '10',
            artifactDaysToKeepStr: '3',
            artifactNumToKeepStr: '5'
        ))
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Setup Tools') {
            steps {
                // Встановлюємо сканер
                bat "dotnet tool install --global dotnet-sonarscanner --ignore-failed-sources || dotnet tool update --global dotnet-sonarscanner"
            }
        }

        stage('SonarQube Analysis Begin') {
            steps {
                // 'SonarQube' — це назва сервера, яку ви вказали в Manage Jenkins -> System
                withSonarQubeEnv('SonarQube') {
                    // Важливо: begin має бути ПЕРЕД dotnet build
                    bat "dotnet sonarscanner begin /k:\"UsersForms\" /n:\"UsersForms\" /v:\"${env.BUILD_NUMBER}\""
                }
            }
        }

        stage('Restore') {
            steps {
                bat "dotnet restore UsersForms.sln"
            }
        }

        stage('Build') {
            steps {
                // Збірка через dotnet CLI (вона автоматично знайде потрібний MSBuild)
                bat "dotnet build UsersForms.sln --configuration Release --no-restore"
            }
        }

        stage('Test') {
            steps {
                bat "dotnet test UsersForms.sln --configuration Release --no-restore"
            }
        

        stage('Quality Gate') {
            steps {
                timeout(time: 5, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline: false
                }
            }
        }

        stage('Archive') {
            steps {
                // Архівуємо артефакти, як у GitHub Actions
                archiveArtifacts artifacts: '**/bin/Release/**', allowEmptyArchive: true
            }
        }
    }

    post {
        always {
            echo 'Очищення робочого простору...'
            cleanWs() // Видаляє вміст папки workspace на агенті
        }
    }
}
