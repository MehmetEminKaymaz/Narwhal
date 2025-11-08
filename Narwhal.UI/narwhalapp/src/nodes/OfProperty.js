import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';

export default memo(({ data, isConnectable }) => {
  return (
    <div style={{
      height: 180,
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
        <span style={{ fontSize: 14, fontWeight: '500' }}>Of Property</span>
        <img 
          src="https://avatars.githubusercontent.com/u/5429470?s=280&v=4" 
          style={{ width: '40px', height: '40px', borderRadius: '4px' }} 
          alt="Property Icon" 
        />
      </div>

      {/* Content */}
      <div style={{ 
        display: 'flex', 
        height: 'calc(100% - 50px)'
      }}>
        {/* Left Side - Target Ports */}
        <div style={{ 
          flex: 1, 
          padding: '10px',
          borderRight: '1px solid #dee2e6',
          backgroundColor: '#f8f9fa',
          position: 'relative'
        }}>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '20px', paddingTop: '20px' }}>
            <div style={{ 
              display: 'flex', 
              alignItems: 'center',
              position: 'relative',
              paddingLeft: '20px'
            }}>
              <Handle
                id="sourceHandle"
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
              <span style={{ color: '#495057', fontSize: '13px', marginLeft: '8px' }}>Source</span>
            </div>

            <div style={{ 
              display: 'flex', 
              alignItems: 'center',
              position: 'relative',
              paddingLeft: '20px'
            }}>
              <Handle
                id="nameHandle"
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
              <span style={{ color: '#495057', fontSize: '13px', marginLeft: '8px' }}>Name</span>
            </div>

            <div style={{ 
              display: 'flex', 
              alignItems: 'center',
              position: 'relative',
              paddingLeft: '20px'
            }}>
              <Handle
                id="setValueHandle"
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
              <span style={{ color: '#495057', fontSize: '13px', marginLeft: '8px' }}>SetValue</span>
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
          <div style={{ display: 'flex', flexDirection: 'column', gap: '20px', paddingTop: '20px' }}>
            <div style={{ 
              display: 'flex', 
              alignItems: 'center',
              position: 'relative',
              paddingRight: '20px',
              justifyContent: 'flex-end'
            }}>
              <span style={{ color: '#495057', fontSize: '13px', marginRight: '8px' }}>Value</span>
              <Handle
                id="valueHandle"
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
      </div>
    </div>
  );
});