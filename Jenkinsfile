pipeline {
    agent { label 'winserver' }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                // Використовуємо сучасний dotnet restore замість nuget.exe
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
        }

        stage('Archive') {
            steps {
                // Архівуємо артефакти, як у GitHub Actions
                archiveArtifacts artifacts: '**/bin/Release/**', allowEmptyArchive: true
            }
        }
        post {
            always {
                echo 'Очищення робочого простору...'
                cleanWs() // Видаляє вміст папки workspace на агенті
            }
        }
    }
}
