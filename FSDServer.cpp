#include <iostream>
#include <sys/socket.h>
#include <netinet/in.h>
#include <sys/types.h>
#include <stdio.h>
#include <unistd.h>
#include <string.h>
#include <stdlib.h>

using namespace std;

void error(const char *msg) {
    perror(msg);
    exit(1);
}

int main() {
    cout << "PTSIM FSD Server Version 1.0" << endl;
    cout << "© 2022. Developed by Jackson M. and Samuel V." << endl;
    cout << "Starting Server. Please wait...." << endl;

    int sockfd, newsockfd, portno;
    socklen_t clilen;
    char buffer[256];
    struct sockaddr_in serv_addr, cli_addr;
    int n;
    
    //create a socket
    //Usage: socket(int domain, int type, int protocol)
    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if(sockfd < 0) {
        error("ERROR opening socket");
    }

    //clear address stucture
    bzero((char *) &serv_addr, sizeof(serv_addr));

    portno = 6809;

    //setup address sturcture
    //server byte order
    serv_addr.sin_family = AF_INET;

    //Autofill with current host's IP
    serv_addr.sin_addr.s_addr = INADDR_ANY;


    //convert short int value for port to network byte order
    serv_addr.sin_port = htons(portno);

    //bind socket to address
    if (bind(sockfd, (struct sockaddr *) &serv_addr, sizeof(serv_addr)) < 0) {
        error("ERROR on binding");
    }

    //listen to all incoming connections
    listen(sockfd,5);

    cout << "Server started!" << endl;

    //accept a connection
    newsockfd = accept(sockfd, (struct sockaddr * ) &cli_addr, &clilen);

    if (newsockfd < 0) {
        error("ERROR on accept");
    }

    cout << "Connection Established!" << endl;

    send(newsockfd, "$DISERVER:CLIENT:VATSIM FSD V3.13:f8b0cdfaeba32a49657632", 56, 0); 

    bzero(buffer,256);

    n = read(newsockfd, buffer, 255);
    if (n < 0) {
        error("ERROR reading from socket");
    }

    cout << "Message from client: " << n << endl;

    return 0;
}