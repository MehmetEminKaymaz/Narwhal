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
    
    <div class="container" style={{height:190, backgroundColor:'gray'}}>
        <div class="header" style={{height:'50px'}}>
               <span style={{ fontSize:12 }}> AWS-VPC : {data.name}</span>
        </div>
        <div class="content">
            <div class="column" style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
                <p style={{color:'black'}}>Cidr Block</p>
                <label>
                    <input type="checkbox" />
                    <span style={{color:'black'}}>Enable DNS Hostnames</span>
                </label>
                <br/>
                <p style={{color:'black',textAlign:'center'}}>Create</p>
            </div>
        </div>
        <Handle id="enableDNSHostNamesHandle" type="target" position={Position.Left} style={{top:83,width:20}}></Handle>
        <Handle id="vpcCreateHandle" type="source" position={Position.Bottom} style={{width:20,backgroundColor:'red'}}></Handle>
    </div>
  );
});