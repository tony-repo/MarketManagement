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
