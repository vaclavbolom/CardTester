// Classification: Commercial confidential 
// Copyright (c) 2024, IDEMIA

#ifndef ___ACFMACHINEINTERFACE_TYPES_HPP
#define ___ACFMACHINEINTERFACE_TYPES_HPP


enum CODING_RESULT
{
   CODING_RESULT_SYSTEM_ERROR    = -2,    //coding result was bad, reject chip. Meaning: This process is not operational (any more). Stop using this process.
   CODING_RESULT_REJECT          = -1,    //coding result was bad, reject chip.
   CODING_RESULT_UNINITIALIZED   = 0,     //default value
   CODING_RESULT_OK                       //coding result was OK
};

#endif // ___ACFMACHINEINTERFACE_TYPES_HPP

//================================================//
// End 
//================================================//