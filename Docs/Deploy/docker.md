# Issues when deploy project

1. Installing vim in docker container

    * Using below commands to install vim

    ``` bash
    #!/bin/bash
        docker exec -it [containerId]
        apt-get update
        apt-get install vim

    ```

2. Deploy vue to nginx, face 403 forbid error

   * The reason for this error is a mistake when set up nginx.conf with the wrong location

   * The root in location should be pointed to your vue files

   ``` bin
    location / {
      root   /usr/share/nginx/html/;
      index  index.html;
      try_files $uri $uri/ /index.html;
    }
   ```

3. Docker connect with each containers
  a: link: this way should creatre container in order

    ``` docker

      docker run -it --name market sqlserver:latest
      docker run -it --name marketmanagement --link market:market marketmanagement:latest

      --link: the first is container name, the second is alias for contains

    ```

  b: bridge network

  ``` docker

      1. docker network create testnet
      2. docker run -it --name <container> ---network <bridge> --network-alias <alias> <image name>
      For example:
        docker run -it --name market --network testnet --network-alias market sqlserver:latest
        docker run -it --name marketmanagement--network testnet --network-alias marketmanagement marketmanagement:latest 
  ```
