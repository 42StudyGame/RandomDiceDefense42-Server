#include <iostream>
#include <winsock2.h>
//#include "ITcpNetwork.h"
#include "hiredis.h"
#include "RedisManager.h"

#define IP "127.0.0.1"
#define PORT 6379
#define CHANNEL "test-channel"

int main(int argc, char** argv)
{
	uint16_t isunix = 0;
	
	if (argc > 2) {
		if (*argv[2] == 'u' || *argv[2] == 'U') {
			isunix = 1;
			/* in this case, host is the path to the unix socket */
			std::cout << "Will connect to unix socket @" + std::string(IP) << std::endl;
		}
	}

	std::unique_ptr<RedisManager> redis = std::make_unique<RedisManager>(isunix, IP, PORT);
	redis->RedisInit();
	while (1)
	{		
		redis->getUsersTask();
		Sleep(1000);
	}
	
	return 0;
}