# Catalog

### Getting Started
1. Download redis to WSL and run this command:
`sudo service redis-server start`. Tutorial how to download here: https://docs.microsoft.com/en-us/windows/wsl/tutorials/wsl-database
2. Download and install docker: https://www.docker.com/
3. Run docker and go to the folder ***Deploys*** (in the CMD / Git BASH) in the root of this repository and start command `docker-compose up`
. If you get a error like ``[1]: max virtual memory areas vm.max_map_count [65530] is too low, increase to at least [262144 docker]``, then you need to run these commands in PowerShell: 1. `wsl -d docker-desktop`; 2. `sysctl -w vm.max_map_count=262144`
4. If you get a error you need to retry the run from CMD / Git BASH `docker-compose-up` in the folder ***Deploys***