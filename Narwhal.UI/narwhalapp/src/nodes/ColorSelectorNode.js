import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';

export default memo(({ data, isConnectable }) => {

    var margin = 10;
    var pStyleList = [];
    for(var i = 0 ; i<data.nodeSize; i++){
        var k = 0;
        if(i===0){
            k = 0;
        }else{
            k=margin;
        }
        pStyleList.push(<p style={{ marginTop: k }}>Satır {i}</p>)
    }
    

    var pHandleList = [];
    for(var i=0;i<data.nodeSize;i++){
        pHandleList.push(<Handle id={"t"+i}  type="source" position={Position.Left} style={{top:50+30*(i+1)}} ></Handle>)
    }

  return (
    
      <div class="container" style={{height:500}}>
        <div class="header" style={{height:'50px'}}>
                Bu header içeriği 
        </div>
        <div class="content">
        <div class="column" style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
            {pStyleList}
        </div>
        <div class="column" style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
            <p style={{ marginTop: 12*1 }}>Satır 1</p>
            <p style={{ marginTop: 12*2 }}>Satır 2</p>
            <p style={{ marginTop: 12*3 }}>Satır 3</p>
        </div>
        <div class="column" style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
            <p style={{ marginTop: 12*1 }}>Satır 1</p>
            <p style={{ marginTop: 12*2 }}>Satır 2</p>
            <p style={{ marginTop: 12*3 }}>Satır 3</p>
        </div>
        <div class="column" style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
            <p style={{ marginTop: 12*1 }}>Satır 1</p>
            <p style={{ marginTop: 12*2 }}>Satır 2</p>
            <p style={{ marginTop: 12*3 }}>Satır 3</p>
        </div>
        </div>
        {pHandleList}
    </div>
  );
});