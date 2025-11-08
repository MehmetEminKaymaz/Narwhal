import React, { useState, useEffect, useCallback } from 'react';
import ReactFlow, { useNodesState, useEdgesState, addEdge, MiniMap, Controls } from 'reactflow';
import 'reactflow/dist/style.css';
import { v4 as uuid } from "uuid";

import ColorSelectorNode from '../../nodes/ColorSelectorNode';

import '../../index.css';
import AwsEC2instance from '../../nodes/AwsEC2instance';
import AwsAuth from '../../nodes/AwsAuth';
import StringNode from '../../nodes/StringNode';
import AwsVolume from '../../nodes/AwsVolume';
import AwsVpc from '../../nodes/AwsVpc';
import DockerConfig  from '../../nodes/DockerConfig';
import DockerContainer from '../../nodes/DockerContainer';
import DockerImage from '../../nodes/DockerImage';
import DockerNetwork from '../../nodes/DockerNetwork';
import DockerPlugin from '../../nodes/DockerPlugin';
import DockerRegistryImage from '../../nodes/DockerRegistryImage';
import DockerSecret from '../../nodes/DockerSecret';
import DockerService from '../../nodes/DockerService';
import DockerTag from '../../nodes/DockerTag';
import DockerVolume from '../../nodes/DockerVolume';
import ConfigNode from '../../nodes/ConfigNode';
import OfProperty from '../../nodes/OfProperty';
const myMethod = (e) => {
  console.log(e);
};
const initBgColor = '#1A192B';

const connectionLineStyle = { stroke: '#fff' };
const snapGrid = [20, 20];
const nodeTypes = {
  selectorNode: ColorSelectorNode,
  awsInstanceNode : AwsEC2instance,
  awsAuthNode : AwsAuth,
  stringNode : StringNode,
  awsVolumeNode : AwsVolume,
  awsVpcNode: AwsVpc,
  dockerConfigNode : DockerConfig,
  dockerContainerNode: DockerContainer,
  dockerImageNode: DockerImage,
  dockerNetworkNode: DockerNetwork,
  dockerPluginNode: DockerPlugin,
  dockerRegistryImageNode: DockerRegistryImage,
  dockerSecretNode: DockerSecret,
  dockerServiceNode: DockerService,
  dockerTagNode : DockerTag,
  dockerVolumeNode: DockerVolume,
  configNode : ConfigNode,
  ofProperty : OfProperty
};

const defaultViewport = { x: 0, y: 0, zoom: 1.5 };

const initialButtonData = [
  {
    id: null,
    name: null,
    type: null,
    data: null,
    style: null
  }
];

function InternalPage(){
  const [nodes, setNodes, onNodesChange] = useNodesState([]);
  const [edges, setEdges, onEdgesChange] = useEdgesState([]);
  const [bgColor, setBgColor] = useState(initBgColor);
  const [buttonData, setButtonData] = useState(initialButtonData);

  const [inputValue, setInputValue] = useState('');

  // Input değiştiğinde çağrılacak fonksiyon
  const handleInputChange = (e) => {
    setInputValue(e.target.value); // Input değeri güncelleniyor
  };


    const compile = () => {
      const compileModel = {
        Nodes: [/* Nodes array içeriği */],
        Edges: [/* Edges array içeriği */],
        RepositoryName: inputValue
      };

      nodes.forEach(e=>console.log(e.data.sourceDataValue));

      //nodes içindekileri compileModel.Nodes'a at
      nodes.forEach(node => {
        compileModel.Nodes.push({
          Id: node.id,
          Name:null,
          Type: node.type,
          Data: node.data.toString(),
          Style: node.style.toString(),
          ColregNodeId: node.colregId,
          SourceDataValue: node.data.sourceDataValue,
        });
      });
      //edges içindekileri compileModel.Edges'a at
      edges.forEach(edge => {
        compileModel.Edges.push({
          Id: edge.id,
          SourceId: edge.source,
          SourceHandle : edge.sourceHandle,
          TargetHandle : edge.targetHandle,
          TargetId: edge.target,
        })});

  // Add your compile logic here
      //send post request to url
      fetch('http://localhost:5000/Compiler/Compile', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem('token')
        },
        body: JSON.stringify(compileModel),
      })
        .then(response => response.json())
        .then(data => console.log(data));


    
    };
  
    const deleteAll = () => {
      // Add your delete all logic here
      //clear nodes and edges
      setNodes([]);
      setEdges([]);
    };
  
    const save = () => {
      const compileModel = {
        Nodes: [/* Nodes array içeriği */],
        Edges: [/* Edges array içeriği */]
      };

      //nodes içindekileri compileModel.Nodes'a at
      nodes.forEach(node => {
        console.log(node);
        compileModel.Nodes.push({
          Id: node.id,
          Name:null,
          Type: node.type,
          Data: node.data.toString(),
          Style: node.style.toString(),
          ColregNodeId: node.colregId,
          SourceDataValue: node.sourceDataValue,
        });
      });
      //edges içindekileri compileModel.Edges'a at
      edges.forEach(edge => {
        compileModel.Edges.push({
          Id: edge.id,
          SourceId: edge.source,
          SourceHandle : edge.sourceHandle,
          TargetHandle : edge.targetHandle,
          TargetId: edge.target,
        })});

  // Add your compile logic here
      //send post request to url
      fetch('/save', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem('token')
        },
        body: JSON.stringify(compileModel),
      })
        .then(response => response.json())
        .then(data => console.log(data));
    };

  useEffect(() => {
    fetch('http://localhost:5000/Compiler/AvailableNodes', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      }
    })
      .then(response => response.json())
      .then(data => setButtonData(data));
  

    setEdges([]);
  }, []);

  const onConnect = useCallback(
    (params) =>
      setEdges((eds) => addEdge({ ...params, animated: true, style: { stroke: '#fff' } }, eds)),
    []
  );

  const addNode = (button) => {
    console.log(button);
    const newNode = {
      
        id: uuid(),
        name: button.name,
        type: button.type,
        data: { nodeSize:10, sourceData:button.sourceDataValue},
        style: { border: '1px solid #777', padding: 5 },
        position: { x: 300, y: 50 },
        colregId: button.colregNodeId,
        sourceDataValue : button.sourceDataValue,
      
    };
    console.log(newNode);
    setNodes((nodes) => nodes.concat(newNode));
    console.log(nodes);
    console.log(edges);
  };

  //const buttons = Array.from({ length: 50 }, (_, i) => i + 1);
  return (
    <div style={{ display: 'flex', flexDirection: 'column', height: '50rem' }}>
      <input 
        type="text" 
        value={inputValue} 
        onChange={handleInputChange} 
        style={{ marginRight: '15px' }} // Checkbox ile input arası boşluk
      />
      <div style={{ display: 'flex', flex: 1, overflowY: 'scroll' }}>
        <div style={{ display: 'flex', flexDirection: 'column' }}>
          {buttonData.map((button) => (
            <button class="btn btn-accent" style={{ margin: '2px' }} key={button} onClick={() => addNode(button)}>
              {button.name}
            </button>
          ))}
        </div>
        <ReactFlow
          nodes={nodes}
          edges={edges}
          onNodesChange={onNodesChange}
          onEdgesChange={onEdgesChange}
          onConnect={onConnect}
          style={{ background: bgColor }}
          nodeTypes={nodeTypes}
          connectionLineStyle={connectionLineStyle}
          snapToGrid={true}
          snapGrid={snapGrid}
          defaultViewport={defaultViewport}
          fitView
          attributionPosition="bottom-left"
        >
          <MiniMap
            nodeStrokeColor={(n) => {
              if (n.type === 'input') return '#0041d0';
              if (n.type === 'selectorNode') return bgColor;
              if (n.type === 'output') return '#ff0072';
            }}
            nodeColor={(n) => {
              if (n.type === 'selectorNode') return bgColor;
              return '#fff';
            }}
          />
          <Controls />
        </ReactFlow>
      </div>
      <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
        <button className='btn btn-outline btn-success' onClick={compile} style={{ margin: '2px' }}>Compile</button>
        <button className='btn btn-outline btn-error' onClick={deleteAll} style={{ margin: '2px' }}>Delete All</button>
        <button className='btn btn-outline btn-warning' onClick={save} style={{ margin: '2px' }}>Save</button>
      </div>
    </div>
  );
}

export default InternalPage