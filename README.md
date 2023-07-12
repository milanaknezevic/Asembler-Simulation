# Asembler-Simulation, Faculty of Electrical Engineering, 2021
Application specification

A simple console application that represents a simulator of its own instruction set architecture with at least 4 general-purpose registers of length 64 bits (it is allowed to use a data type of length 8 bytes, e.g. long long or uint64_t for registers). 
The simulator should function as an interpreter. 
It should be possible to source assembly code is loaded from a file. 
Correctly perform the necessary lexical, syntactic and semantic analysis code.
The instruction set of the simulated machine (guest) must include:

    Abasic arithmetic operations (ADD, SUB, MUL, DIV)
    Any live cell with two or three live neighbours lives on to the next generation.
    Any live cell with more than three live neighbours dies, as if by overpopulation.
    Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

The instruction set of the simulated machine (guest) must include:
 basic arithmetic operations (ADD, SUB, MUL, DIV)
  basic bit logic operations (AND, OR, NOT, XOR)
  instruction for moving data between registers (MOV)
  an instruction to enter data from standard input (similar to the corresponding system call)
  an instruction to print data to standard output (similar to the corresponding system call)
  
  
Implement simple single-step debugging support. 
Enable execution and review of values of all registers and specified memory addresses at set breakpoints in assembly language code at runtime. 
In this mode, the transition to the next instruction (NEXT or STEP console commands) and transition to the next breakpoint (CONTINUE).
