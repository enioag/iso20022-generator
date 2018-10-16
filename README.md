# iso20022-generator
The iso20022-generator is a c# library which helps you generating iso 20022 artifacts with the minimum amount of information you need to set up valid documents.

##### What you can do at the moment:
  - Currently the creation of pain.001 files is the only thing the library can do.
  - The file can contains multiple transactions to different receivers. 

##### Limitations:
  - All the transactions in the file have the same execution date
  - Although the iso20022 is a standard definition it looks as there are a lot of excess to this standard in the world. The library was mainly built for communication with swiss banks. To be precise the library was successfully tested against the test environments from the following banks: Raiffeisen Bank Schweiz, Credit Suisse and Zürcher Kantonalbank
  
##### Usage: 
have a look into the happy [test file](https://github.com/enioag/iso20022-generator/blob/master/iso20022-generator/generator-test/GeneratorTest.cs)