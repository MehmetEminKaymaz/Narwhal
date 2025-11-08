import React, { memo, useState, setState } from 'react';
import { Handle, Position } from 'reactflow';

export default memo(({ data, isConnectable}) => {
  

  const handleInputChange = (event) => {
    data.sourceDataValue = event.target.value; // Update the node data
    console.log(data.sourceDataValue);
  };
  return (
     
    <div class="container" style={{height:60,width:200, backgroundColor:'#65F0A4',borderRadius:'10px'}}>
    
           
            <input id="text" name="text" style={{marginTop:20,width:150,marginLeft:25}} value={data.sourceData} onChange={handleInputChange}></input>
         
       
        <Handle id="stringHandle" type="source" position={Position.Right} style={{top:33,width:10,backgroundColor:'green'}}></Handle>
    </div>
  );
});