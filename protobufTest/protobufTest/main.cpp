#include <iostream>
#include <google/protobuf/stubs/common.h>
#include <fstream>
#include <string>
#include "protobufTest.h"

using namespace std;

int main(int argc, char* argv[])
{
    // Verify that the version of the library that we linked against is
    // compatible with the version of the headers we compiled against.
    GOOGLE_PROTOBUF_VERIFY_VERSION;

    {
        if (argc != 2) {
            cerr << "Usage:  " << argv[0] << " ADDRESS_BOOK_FILE" << endl;
            return -1;
        }

        tutorial::AddressBook address_book;

        {
            // Read the existing address book.
            fstream input(argv[1], ios::in | ios::binary);
            if (!input) {
                cout << argv[1] << ": File not found.  Creating a new file." << endl;
            }
            else if (!address_book.ParseFromIstream(&input)) {
                cerr << "Failed to parse address book." << endl;
                return -1;
            }
        }

        // Add an address.
        PromptForAddress(address_book.add_people());
    
        {
            // Write the new address book back to disk.
            fstream output(argv[1], ios::out | ios::trunc | ios::binary);
            if (!address_book.SerializeToOstream(&output)) {
                cerr << "Failed to write address book." << endl;
                return -1;
            }
        }
    }
    {
        tutorial::AddressBook address_book;

        {
            // Read the existing address book.
            fstream input(argv[1], ios::in | ios::binary);
            if (!address_book.ParseFromIstream(&input)) {
                cerr << "Failed to parse address book." << endl;
                return -1;
            }
        }
        ListPeople(address_book);
    }

    // Optional:  Delete all global objects allocated by libprotobuf.
    google::protobuf::ShutdownProtobufLibrary();

    return 0;
}