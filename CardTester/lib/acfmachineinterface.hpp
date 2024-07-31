// Classification: Commercial confidential 
// Copyright (c) 2024, IDEMIA

#ifndef ___ACFMACHINEINTERFACE_HPP
#define ___ACFMACHINEINTERFACE_HPP

#ifdef __cplusplus
extern "C"
{
#endif /*__cplusplus*/

#define ACF_MACHINE_INTERFACE_VERSION 1

#include "acfmachineinterface_types.hpp"

//================================================//
// Export the 3 MachineInterface functions
//================================================//
#ifdef MACHINEINTERFACE_EXPORTS
#define MACHINEINTERFACE_API __declspec(dllexport)
#else
#define MACHINEINTERFACE_API __declspec(dllimport)
#endif

#define JOBSTART_RESULT_BUFFERSIZE 1024


/**
 * JobStart. Initializes this process for a new job.
 * Each JobStart call should have a matching call to JobEnd. Do not perform more than a single JobStart after each other.
 *
 * If after a call to JobStart iErrorMessage has been updated to a higher value, the error message was bigger then the allocated size of cErrorMessage.
 * Allocate cErrorMessage to this size and call JobStart again. This call will only update cErrorMessage without any other functional update to this process.
 *
 * cJobName          input: job name (buffer)(jobfilename)
 * iJobNameSize      input: job name (size)
 * cWoid             input: Workorder ID (buffer)
 * iWoid             input: Workorder ID (size)
 * eResult           output: result of operation (for this call the value REJECT is not used)
 * cErrorMessage     output: (only when eResult != CODING_RESULT_OK) (only when cErrorMessage >= size of error message) will be filled with error message to display
 * iErrorMessage     input&output: (for input) should be filled with the allocated size of the cErrorMessage buffer. 
 *                             (for output) is updated with the length of error string which this process wants to write in cErrorMessage
 *
 * return value, the handle required for the other interface methods.
 */
extern "C" MACHINEINTERFACE_API 
int JobStart(  char*          cJobName,
               int            iJobNameSize,
               char*          cWoid,
               int            iWoid,
               CODING_RESULT& eResult,
               char*          cErrorMessage,    //for efficiency: please allocate a buffer with a size of JOBSTART_RESULT_BUFFERSIZE
               int&           iErrorMessage);

/**
 * CardRun. Perform all chip activities for a station
 * JobStart shall be called before the first call to CardRun.
 *
 * If after a call to CardRun iErrorMessage has been updated to a higher value, the error message was bigger then the allocated size of cErrorMessage.
 * Allocate cErrorMessage to this size and call JobStart again. This call will only update cErrorMessage without any other functional update to this process.
 * hHandle       input: the handle from JobStart
 * iCardId       input: the unique ID (within this job) for this document/chip
 * iBendCounter  input: the number of bend performed before calling this funtion
 * eResult       output: result of operation
 * cErrorMessage output: (only when eResult != CODING_RESULT_OK) (only when cErrorMessage >= size of error message) will be filled with error message to display
 * iErrorMessage input&output: (for input) should be filled with the allocated size of the cErrorMessage buffer. 
 *                             (for output) is updated with the length of error string which this process wants to write in cErrorMessage
 */
extern "C" MACHINEINTERFACE_API 
void   CardRun(const int       hHandle,
               int             iCardId,
               int             iBendCounter,
               CODING_RESULT&  eResult,
               char*           cErrorMessage,      //for efficiency: please allocate a buffer with a size of CARDRUN_RESULT_BUFFERSIZE
               int&            iErrorMessage); 

/**
 * JobEnd. Ends a job, making this process ready to accept a new job.
 *
 * hHandle   input&output: (for input) the handle, which originates from the return value of JobStart. 
 *                        (for output) will be reset to 0.
 */ 
extern "C" MACHINEINTERFACE_API 
void   JobEnd(  int& hHandle);

#ifdef __cplusplus
}
#endif /*__cplusplus*/

#endif // ___ACFMACHINEINTERFACE_HPP

//================================================//
// End 
//================================================//