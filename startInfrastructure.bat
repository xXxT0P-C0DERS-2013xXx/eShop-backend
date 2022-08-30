call wsl sudo service redis-server start
call cd Deploys
call wsl -d docker-desktop sysctl -w vm.max_map_count=262144
call docker-compose up
PAUSE