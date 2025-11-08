import React, { memo, useState, setState } from 'react';
import { Handle, Position } from 'reactflow';

export default memo(({ data, isConnectable}) => {
  

  const handleInputChange = (event) => {
    data.sourceDataValue = event.target.value; // Update the node data
    console.log(data.sourceDataValue);
  };
  return (
     
    <div class="container" style={{height:300,width:200, backgroundColor:'#db0f38',borderRadius:'10px'}}>
    
           
    <textarea
        value={data.sourceData}
        onChange={handleInputChange}
        style={{ width: '95%', height: '280px', resize: 'none', alignSelf:'center' , marginTop: 10 }}
        placeholder="Write Config Here..."
      />
         
       
        <Handle id="stringHandle" type="source" position={Position.Right} style={{top:150,width:10,backgroundColor:'red'}}></Handle>
    </div>
  );
});