call cd Deploys
call wsl -d docker-desktop sysctl -w vm.max_map_count=262144
call docker-compose build
call docker-compose up
PAUSE