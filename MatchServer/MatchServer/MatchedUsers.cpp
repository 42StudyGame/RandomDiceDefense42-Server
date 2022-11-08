
#include "MatchedUsers.h"

MatchedUsers::MatchedUsers(std::string uid1, std::string uid2, std::string ip, std::string port)
    : m_uid1(uid1), m_uid2(uid2), m_ip(ip), m_port(port)
{}

std::string MatchedUsers::makeJson()
{
    return "{\"uid1\":\"" + m_uid1 + "\","
        + "\"uid2\":" + m_uid2 + "\","
        + "\"ip\":" + m_ip + "\","
        + "\"port\":" + m_port + "\"}";
}
