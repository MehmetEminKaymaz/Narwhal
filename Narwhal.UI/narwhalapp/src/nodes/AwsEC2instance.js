import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';

export default memo(({ data, isConnectable }) => {

  return (
    
    <div class="container" style={{height:377, backgroundColor:'gray'}}>
        <div class="header" style={{height:'50px'}}>
               <span style={{ fontSize:12}}> AWS-EC2-INSTANCE : {data.Prefix}{data.AwsInstanceName}</span>
        </div>
        <div class="content">
            <div class="column" style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
                <div>Instance Type</div>
                <select>
                    <option key="c6i.2xlarge" value="c6i.2xlarge">
                    c6i.2xlarge
                    
                    </option>
                    <option key="t2.medium" value="t2.medium">
                    t2.medium
                    </option>
                </select>
                <p style={{color:'black'}}>Auth</p>
                <p style={{color:'black'}}>Volume</p>
                <p style={{color:'black'}}>VPC</p>
                <p style={{color:'black'}}>Ingress Ports</p>
                <p style={{color:'black'}}>Egress Ports</p>
                <p style={{color:'black'}}>Remote Exec</p>
                <p style={{color:'black'}}>Source File</p>
                <label>
                    <input type="checkbox" />
                    <span style={{color:'black'}}>Delete On Termination</span>
                </label>
                <br/>
                <p style={{color:'black',textAlign:'center'}}>Contains</p>

            </div>
        </div>
        <Handle id="authHandle" type="target" position={Position.Left} style={{top:135}}></Handle>
        <Handle id="volumeHandle" type="target" position={Position.Left} style={{top:158}}></Handle>
        <Handle id="vpcHandle" type="target" position={Position.Left} style={{top:183}}></Handle>
        <Handle id="ingressPortHandle" type="target" position={Position.Left} style={{top:208}}></Handle>
        <Handle id="egressPortHandle" type="target" position={Position.Left} style={{top:232}}></Handle>
        <Handle id="remoteExecHandle" type="target" position={Position.Left} style={{top:256}}></Handle>
        <Handle id="sourceFileHandle" type="target" position={Position.Left} style={{top:279}}></Handle>
        <Handle id="awsContainsHandle" type="target" position={Position.Bottom} ></Handle>
    </div>
  );
});