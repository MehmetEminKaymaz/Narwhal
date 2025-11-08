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
    <div style={{
      height: 220,
      width: 340,
      backgroundColor: '#f8f9fa',
      borderRadius: '5px',
      border: '1px solid #dee2e6'
    }}>
      {/* Header */}
      <div style={{ 
        display: 'flex', 
        alignItems: 'center', 
        justifyContent: 'space-between',
        height: '50px',
        padding: '0 10px',
        borderBottom: '1px solid #dee2e6',
        backgroundColor: '#e9ecef'
      }}>
        <span style={{ fontSize: 14, fontWeight: '500' }}>Docker Tag</span>
        <img 
          src="https://avatars.githubusercontent.com/u/5429470?s=280&v=4" 
          style={{ width: '40px', height: '40px', borderRadius: '4px' }} 
          alt="Docker Logo" 
        />
      </div>

      {/* Content */}
      <div style={{ 
        display: 'flex', 
        height: 'calc(100% - 50px)',
        position: 'relative'
      }}>
        {/* Left Side - Target Ports */}
        <div style={{ 
          flex: 1, 
          padding: '10px',
          borderRight: '1px solid #dee2e6',
          backgroundColor: '#f8f9fa',
          position: 'relative'
        }}>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '15px', paddingTop: '15px' }}>
            <div style={{ 
              display: 'flex', 
              alignItems: 'center',
              position: 'relative',
              paddingLeft: '20px'
            }}>
              <Handle
                id="sourceImageHandle"
                type="target"
                position={Position.Left}
                style={{ 
                  background: '#6c757d',
                  width: '16px',
                  height: '16px',
                  left: '-8px',
                  border: '2px solid #ffffff',
                  boxShadow: '0 0 0 1px #6c757d'
                }}
                isConnectable={isConnectable}
              />
              <span style={{ color: '#495057', fontSize: '13px', marginLeft: '8px' }}>Source Image</span>
            </div>

            <div style={{ 
              display: 'flex', 
              alignItems: 'center',
              position: 'relative',
              paddingLeft: '20px'
            }}>
              <Handle
                id="targetImageHandle"
                type="target"
                position={Position.Left}
                style={{ 
                  background: '#6c757d',
                  width: '16px',
                  height: '16px',
                  left: '-8px',
                  border: '2px solid #ffffff',
                  boxShadow: '0 0 0 1px #6c757d'
                }}
                isConnectable={isConnectable}
              />
              <span style={{ color: '#495057', fontSize: '13px', marginLeft: '8px' }}>Target Image</span>
            </div>
          </div>
        </div>

        {/* Right Side - Source Ports */}
        <div style={{ 
          flex: 1, 
          padding: '10px',
          backgroundColor: '#f8f9fa',
          position: 'relative'
        }}>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '15px', paddingTop: '15px' }}>
            <div style={{ 
              display: 'flex', 
              alignItems: 'center',
              position: 'relative',
              paddingRight: '20px',
              justifyContent: 'flex-end'
            }}>
              <span style={{ color: '#495057', fontSize: '13px', marginRight: '8px' }}>Id</span>
              <Handle
                id="idHandle"
                type="source"
                position={Position.Right}
                style={{ 
                  background: '#dc3545',
                  width: '16px',
                  height: '16px',
                  right: '-8px',
                  border: '2px solid #ffffff',
                  boxShadow: '0 0 0 1px #dc3545'
                }}
                isConnectable={isConnectable}
              />
            </div>

            <div style={{ 
              display: 'flex', 
              alignItems: 'center',
              position: 'relative',
              paddingRight: '20px',
              justifyContent: 'flex-end'
            }}>
              <span style={{ color: '#495057', fontSize: '13px', marginRight: '8px' }}>Source Image Id</span>
              <Handle
                id="sourceImageIdHandle"
                type="source"
                position={Position.Right}
                style={{ 
                  background: '#dc3545',
                  width: '16px',
                  height: '16px',
                  right: '-8px',
                  border: '2px solid #ffffff',
                  boxShadow: '0 0 0 1px #dc3545'
                }}
                isConnectable={isConnectable}
              />
            </div>
          </div>
        </div>

        {/* Bottom Center - Use Handle */}
        <div style={{
          position: 'absolute',
          bottom: '-8px',
          left: '50%',
          transform: 'translateX(-50%)',
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          gap: '4px'
        }}>
          <span style={{ 
            color: '#495057', 
            fontSize: '13px',
            backgroundColor: '#f8f9fa',
            padding: '2px 6px',
            borderRadius: '4px',
            border: '1px solid #dee2e6',
            marginBottom: '8px'
          }}>Use</span>
          <Handle
            id="useHandle"
            type="source"
            position={Position.Bottom}
            style={{ 
              background: '#dc3545',
              width: '16px',
              height: '16px',
              border: '2px solid #ffffff',
              boxShadow: '0 0 0 1px #dc3545',
              bottom: '-8px'
            }}
            isConnectable={isConnectable}
          />
        </div>
      </div>
    </div>
  );
});