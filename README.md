# FinTec Demo API

The _FinTec Demo API_ exposes data from financial domain for demonstration purpose. It has been built using .NET Core and is licensed under [MIT License](./LICENSE).

## Dependencies

In order to run the API locally, you either need .NET Core SDK installed or a working Docker setup. 

## Running API on bare metal

If you want to run the API directly on your machine, two script in the root folder will assist. First execute the `build.sh` script to restore dependencies and build the API.

```bash
./build.sh
```

Once the project has been built successfully, run the project using the `start.sh` script.

```bash
./start.sh
```

> NOTE: Both scripts (`build.sh` and `start.sh`) will use Debug configuration


## Running API using Docker

If you've a working Docker installation on your system, you can run the _FinTec Demo API_ using a simple docker container.

To build the Docker image, use the `build-docker.sh` script form the project's root folder.


```bash
./build-docker.sh
```

once the image has been built, you can start a new container from the `fintec-demo-api:development-latest` image using the `start-docker.sh` script (also located in projec's root directory).

The `start-docker.sh` will create a new container and bind the exposed container port to port `8080` on your local machine. Either ensure this port is available or pass an alternative port as argument.

```bash
./start-docker.sh
# optionally provide a custom port
./start-docker.sh 8081
```

