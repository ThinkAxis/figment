# figment

Figment can find file duplicates/file with same signatures

## Features 

1. Creating file hashes for files in a directory
2. Creating a metadata table of the files in the directory
3. Creating an index table based on this data filled with this metadata
4. Displaying duplicacy score for files found to be duplicates of each other.

## Usage 

- Point Figment to a directory to begin processing.
- The tool will generate hashes and metadata for all files within that directory.
- An index is created to efficiently find and report duplicate files based on matching signatures.



## Requirements

- NET 9.0 or later
- Cross-platform: Runs on Windows, macOS, and Linux