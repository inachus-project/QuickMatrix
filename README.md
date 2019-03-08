# QuickMatrix
Quick Matrix is a library meant to provide a quick and easy way to handle matrix and vector operations in C#. The matrix has optimized operators for addition, subtraction, and matrix multiplication. Quick Matrix utilizes several optimization strategies to speed up it's operators.

Quick Matrix includes methods to perform addition, subtraction, matrix multiplication, the dot product, the outer product, and various other utility methods for matrices and vectors. Each method has had several optimizations to increase their speed. Most of the libraries optimizations have currently gone into the matrix multiplication algorithm as matrix multiplication takes drastically longer to run then normal operations.

Quick Matrix's matrix multiplication algorithm uses an optimized naive approach. Basic optimizations like utilizing the transpose of the second matrix and multithreading the algorithm have been added. Extra optimizations include utilizing a one dimensional array to half pointer references and reducing the overall number of memory allocations have also been added.

Quick Matrix has been unit testing and commented to increase the overall usability of the library. The unit tests have been included in the sln folder of the project, as well as the solution used to test the library. The src folder contains the source files for the matrix library, and the dll folder contains the release version of the library.

Future plans include adding equality operators to the library, adding division for matrices and vectors, allowing operators to be multithreaded, and pushing the build onto NuGet.
