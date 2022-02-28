# BadgeMeUp

## Running the App from VS Code with Docker

1. Follow the prerequisites section [here](https://code.visualstudio.com/docs/containers/debug-netcore) to setup Docker
2. run `docker-compose up -d database storage` to start the database and storage emulator
3. run `docker-compose build webapp` to build the web app
4. run `docker-compose up webapp` to run the web app
5. Use the docker attach launch setting to debug the web app

When you're done debugging, run `docker-compose down` to stop the database and storage containers