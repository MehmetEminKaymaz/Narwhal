import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';

/*
name
instance type
ingres Ports
egress Ports



volume type  --> volume
volume size

delete on termination (bool)
remote exec
source file
prefix

access key
secret key --> auth
connection

vpc

*/
export default memo(({ data, isConnectable }) => {

  return (
    
    <div class="container" style={{height:236, backgroundColor:'gray'}}>
        <div class="header" style={{height:'50px'}}>
               <span style={{ fontSize:12 }}> AWS-Volume : {data.name}</span>
        </div>
        <div class="content">
            <div class="column" style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
                <div style={{color: 'black'}}>Volume Type</div>
                <select>
                    <option key="gp2" value="gp2">
                    gp2
                    </option>
                    <option key="gp3" value="gp3">
                    gp3
                    </option>
                </select>
                <p style={{color:'black'}}>Volume Size</p>
                <label>
                    <input type="checkbox" />
                    <span style={{color:'black'}}>Delete On Termination</span>
                </label>
                <br/>
                <p style={{color:'black',textAlign:'center'}}>Attach</p>
            </div>
        </div>
        <Handle id="volumeSizeHandle" type="target" position={Position.Left} style={{top:131,width:20}}></Handle>
        <Handle id="volumeAttachHandle" type="source" position={Position.Bottom} style={{width:20,backgroundColor:'red'}}></Handle>
    </div>
  );
});