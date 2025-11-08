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
    
    <div class="container" style={{height:283, backgroundColor:'gray'}}>
        <div class="header" style={{height:'50px'}}>
               <span style={{ fontSize:12}}> AWS-Auth : {data.name}</span>
        </div>
        <div class="content">
            <div class="column" style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
                <p style={{color:'black'}}>Access Key</p>
                <p style={{color:'black'}}>Secret Key</p>
                <p style={{color:'black'}}>Connection Type</p>
                <p style={{color:'black'}}>Private Key</p>
                <p style={{color:'black'}}>User</p>
                <p style={{color:'black'}}>Host</p>
                <br/>
                <p style={{color:'black',textAlign:'center'}}>Use</p>
            </div>
        </div>
        <Handle id="accessKeyHandle" type="target" position={Position.Left} style={{top:88}}></Handle>
        <Handle id="secretKeyHandle" type="target" position={Position.Left} style={{top:112}}></Handle>
        <Handle id="connectionTypeHandle" type="target" position={Position.Left} style={{top:135}}></Handle>
        <Handle id="privateKeyHandle" type="target" position={Position.Left} style={{top:160}}></Handle>
        <Handle id="userHandle" type="target" position={Position.Left} style={{top:184}}></Handle>
        <Handle id="hostHandle" type="target" position={Position.Left} style={{top:208}}></Handle>
        <Handle id="awsAuthUseHandle" type="source" position={Position.Bottom} style={{backgroundColor:'red',width:20}}></Handle>
    </div>
  );
});