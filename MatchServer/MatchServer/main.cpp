#include <iostream>
//#include "ITcpNetwork.h"
#include "hiredis.h"
#include "Redis.h"

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

	std::unique_ptr<Redis> redis = std::make_unique<Redis>(isunix, IP, PORT, CHANNEL);
	redis->RedisInit();
	for (int i = 0; i < 10; i++)
	{
		redis->Publish("hi");
		Sleep(2000);
	}
	
	return 0;
}