//Demo 1 - Set the correct imports (System and Interop)
//System.Runtime.InteropServices contains the API call for marshal.copy
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krabby_Patty_Demo
{
    internal class Krabby_Patty_Demo
    {
        //Demo 1 - Import NTDLL API Call for VirtualAlloc
        static void Main(string[] args)
        {
            //Demo 1 - Create placeholder for shellcode array

            //Demo 3 - Add hardcoded shellcode to variable.

            //Demo 1 - Create Constants for VirtualAlloc API Call
            //MEM_COMMIT
            //PAGE_EXECUTE_READWRITE

            //Demo 1 - Create API Call for Allocate Memory
            //Note: The API call will return a pointer to our memory region so it will need to be saved to a variable or the code won't compile
            //We will also need that pointer for both the Marshal.Copy and the CreateThread
            /*
             * LPVOID VirtualAlloc(
             *    [in] LPVOID/IntPtr  lpAddress,
             *    [in] SIZE_T/UInt32  dwSize,
             *    [in] DWORD/UInt32   flAllocationType,
             *    [in] DWORD/UInt32   flProtect
             *  );
             */

            //Demo 1 - Create API call to copy shellcode to memory

            /*
             * public static void Copy (float[] source, int startIndex, IntPtr destination, int length);
             */

            //Demo 2 - Create API call to make a thread at our allocated memory region
            //Note: The API returns a hande to the thread so it will need to be saved to a variable or the code will not compile
            //We will also need that handle for the WaitforSingleObject API call.

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

            //Demo 2 - Use the WaitforSingleObject API call to instruct the OS to wait for an infinite amount of time for the thread to finish.

            /*
             * DWORD WaitForSingleObject(
             *  [in] HANDLE (IntPtr)    hHandle,
             *  [in] DWORD  (UInt)      dwMilliseconds
             * );
             */
        }
        /*
         * Teachers Pet Project:  Reasearch how to fix this shellcode runner to never have RWX permissions allocated.  
         * Hint: You will need to add an additional API call to change the memory permissions as well as change the access mask that the memory is allocated with
         */
    }
}
