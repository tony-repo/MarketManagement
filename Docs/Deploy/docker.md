# Issues when deploy project

1. Installing common tools in docker container

    * Using below commands to install common tools

    ``` bash
    #!/bin/bash
        docker exec -it [containerId]
        apt-get update
        apt-get install vim
        apt-get install inetutils-ping

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

4. MarketMnagement server can not connect to mysql

    a: using below command to open mysql container

      ``` bash
        docker exec -it [container name] /bin/bash
      ```

    b: login to the mysql;

      ``` bash
        mysql -h localhost -u root -p
      ```

    c: change caching_sha2_password to mysql_native_password

      ``` bash
        user mysql;
        mysql> ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'password';
        Query OK, 0 rows affected (0.00 sec)

        mysql> ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'password';
        Query OK, 0 rows affected (0.01 sec)  
      ```

    d: check status

      ``` bash
         FLUSH PRIVILEGES;
         SELECT Host, User, plugin from user;
      ```

5. Mysql can not migration

    The reason is we use the wrong EF core library:

    `Pomelo.EntityFrameworkCore.MySql` : we need this one
    `MySql.Data.EntityFrameworkCore` : it does not implement this feature

6. Command for Ef core:

``` bash
    Add-Migration -c YourDbContext [name]
    
    Update-Database

    Update-Database -Migration:0 # detel table schema

    Remove-Migration
```
