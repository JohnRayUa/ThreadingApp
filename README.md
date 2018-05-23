# ThreadingApp

# Description

Multithreaded console application C #. 
The main thread starts X (1 <X <= 64) auxiliary threads. X - the first parameter of the command lines.
Each of the auxiliary flows is infinite (with some changing delay) adds its own element to a common container. 
In this case, it deletes the oldest element in case it was added by another thread or if the number of items in the container> Y (Y is the second command-line parameter). 
The main thread expects any input from the command line (for example, pressing Enter). 
After receiving the input from the command line, the main thread should print statistics of the form <thread> - <number of elements for this stream in the container>
for all threads (auxiliary flows will not be used anymore) and the maximum registered number of items in the container.
After that, the application must exit correctly. Main conditions and requirements:

1. The format of the command line is 'test_app X Y'. 
2. Y is an integer by the amount of which the constraint is not superimposed (within the constraint of type variable). 
3. The code should safely handle possible exceptions, ie it is assumed that any of the streams can throw an exception anywhere in the code. 
In this case, the overall operability of the application must be preserved, or the application must correctly terminated if the exception occurred in the main thread. 
4. The code must be object-oriented. 
5. The application should provide the optimal speed for both accumulation and display statistics.
