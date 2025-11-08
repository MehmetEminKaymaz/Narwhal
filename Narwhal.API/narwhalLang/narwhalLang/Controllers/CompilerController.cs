using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using narwhalLang.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Docker.Provider;
using HashiCorp.Cdktf.Providers.Docker.Image;
using HashiCorp.Cdktf.Providers.Docker.Container;
using System.Reflection;
using HashiCorp.Cdktf.Providers.Docker.Config;
using HashiCorp.Cdktf.Providers.Docker.Network;
using HashiCorp.Cdktf.Providers.Docker.Plugin;
using HashiCorp.Cdktf.Providers.Docker.RegistryImage;
using HashiCorp.Cdktf.Providers.Docker.Secret;
using HashiCorp.Cdktf.Providers.Docker.Service;
using HashiCorp.Cdktf.Providers.Docker.Tag;
using HashiCorp.Cdktf.Providers.Docker.Volume;

using RazorEngine.Templating;
using RazorEngine;
using static narwhalLang.Controllers.CompilerController;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace narwhalLang.Controllers
{

    public class AvailableNodeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public string Style { get; set; }
        public Guid ColregNodeId { get; set; }
        public string SourceDataValue { get; set; }
    }

    public class TestModel
    {
        public Type MainType { get; set; }
        public Type ConfigType { get; set; }
        public string VisualName { get; set; }
        public string VisualType { get; set; }
        public string Description { get; set; }

    }

    [ApiController]
    [Route("[controller]")]
    public class CompilerController : ControllerBase
    {
        private Dictionary<Procedures, string> procedureSource = new()
        {
            {Procedures.StartNameSpace, "namespace @Model.Namespace { "},
            {Procedures.StartClass, "class @Model.ClassName : TerraformStack { public @($\"{Model.ClassName}\")(Construct scope, string id) : base(scope, id) { "},
            {Procedures.AddDockerProvider, "new DockerProvider(this, \"@($\"{Model.Id}\")\", new DockerProviderConfig { });"},
            {Procedures.DefineDockerItemTemplate, "var @($\"n_{Model.VariableIdForNode}\") = new @($\"{Model.NodeClassName}\")(this,\"@($\"{Model.ModelStringId}\")\",new @($\"{Model.NodeClassName}\")Config{" +
                "@foreach(var prop in @Model.ActiveProperties){" +
                "@($\"{prop.Name}={prop.Value},\")" +
                "}});" },
            {Procedures.EndConstructor,"}" },
            {Procedures.EndClass, " } "},
            {Procedures.EndNameSpace, " } "},
            {Procedures.AddStringDefinition, "string @Model.StringId = \"@($\"{Model.Value}\")\";" }
        };

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CompilerController> _logger;
        private readonly DataContext _dbContext;

        public CompilerController(ILogger<CompilerController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dbContext = dataContext;
        }

        void ProcessProperties(Type type, Guid nodeId, Guid? parentPropertyId = null, bool isNested = false)
        {
            // Reflection ile tipin property'lerini al
            PropertyInfo[] properties = type.GetProperties();

            foreach (var prop in properties)
            {
                var propGuid = Guid.NewGuid();

                // Property'yi veritabanýna ekle
                _dbContext.Properties.Add(new NarwhalProperty()
                {
                    Id = propGuid,
                    NarwhalNodeId = nodeId,
                    IsConfigForDockerProvider = false, // Burayý gerektiði gibi ayarlayabilirsin
                    IsNestedProperty = isNested,
                    IsRequired = false,
                    PropertyDefinition = "", // Gerekirse doldur
                    PropertyName = prop.Name,
                    PropertyRuntimeValueType = prop.PropertyType.Name,
                    ParentPropertyId = parentPropertyId // Parent ID, null olabilir
                });

                // Eðer property'nin tipi class ve string deðilse, iç içe property'leri de iþlemek için recursive çaðrý yap
                if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    // Alt property'leri iþle (nested property)
                    ProcessProperties(prop.PropertyType, nodeId, propGuid, true);
                }
            }
        }



        [HttpGet("Test")]
        public bool TestArea()
        {

            var supportedNodeList = new List<TestModel>()
            {
                new TestModel{
                    MainType = typeof(Container),
                    ConfigType = typeof(ContainerConfig),
                    VisualName = "docker_container",
                    VisualType = "dockerContainerNode",
                    Description = "Docker Container"
                },
                new TestModel
                {
                    MainType = typeof(Image),
                    ConfigType = typeof(ImageConfig),
                    VisualName = "docker_image",
                    VisualType = "dockerImageNode",
                    Description = "Docker Image"
                },
                new TestModel
                {
                    MainType = typeof(Config),
                    ConfigType = typeof(ConfigConfig),
                    VisualName = "docker_config",
                    VisualType = "dockerConfigNode",
                    Description = "Docker Config"
                },
                new TestModel
                {
                    MainType = typeof(Network),
                    ConfigType = typeof(NetworkConfig),
                    VisualName = "docker_network",
                    VisualType = "dockerNetworkNode",
                    Description = "Docker Network"
                },
                new TestModel
                {
                    MainType = typeof(Plugin),
                    ConfigType = typeof(PluginConfig),
                    VisualName = "docker_plugin",
                    VisualType = "dockerPluginNode",
                    Description = "Docker Plugin"
                },
                new TestModel
                {
                    MainType = typeof(RegistryImage),
                    ConfigType = typeof(RegistryImageConfig),
                    VisualName = "docker_registry_image",
                    VisualType = "dockerRegistryImageNode",
                    Description = "Docker Registry Image"
                },
                new TestModel
                {
                    MainType = typeof(Secret),
                    ConfigType = typeof(SecretConfig),
                    VisualName = "docker_secret",
                    VisualType = "dockerSecretNode",
                    Description = "Docker Secret"
                },
                new TestModel
                {
                    MainType = typeof(Service),
                    ConfigType = typeof(ServiceConfig),
                    VisualName = "docker_service",
                    VisualType = "dockerServiceNode",
                    Description = "Docker Service"
                },
                new TestModel
                {
                    MainType = typeof(Tag),
                    ConfigType = typeof(TagConfig),
                    VisualName = "docker_tag",
                    VisualType = "dockerTagNode",
                    Description = "Docker Tag"
                },
                new TestModel
                {
                    MainType = typeof(Volume),
                    ConfigType = typeof(VolumeConfig),
                    VisualName = "docker_volume",
                    VisualType = "dockerVolumeNode",
                    Description = "Docker Volume"
                }
            };

            foreach (var tm in supportedNodeList)
            {

                var maintype = tm.MainType;
                var properties = maintype.GetProperties();

                var configType = tm.ConfigType;
                var configProperties = configType.GetProperties();
                var v1 = properties.Select(f => new { Name = f.Name, Type = f.PropertyType, IsConfig = false, IsReadOnly = !f.CanWrite, IsNested = f.GetType().IsPrimitive }).ToList();
                var v2 = configProperties.Select(f => new { Name = f.Name, Type = f.PropertyType, IsConfig = true, IsReadOnly = !f.CanWrite, IsNested = f.GetType().IsPrimitive }).ToList();
                v1.AddRange(v2);

                var nodeId = Guid.NewGuid();
                var narwhalNode = new NarwhalNode()
                {
                    Id = nodeId,
                    Description = tm.Description,
                    IsDataSource = false,
                    IsResource = true,
                    Name = maintype.Name,
                    VisualName = tm.VisualName,
                    VisualType = tm.VisualType,
                };
                _dbContext.Nodes.Add(narwhalNode);

                foreach (var prop in v1)
                {
                    var propGuid = Guid.NewGuid();
                    if (prop.IsNested)
                    {
                        ProcessProperties(prop.Type, nodeId);
                    }
                    else
                    {
                        _dbContext.Properties.Add(new NarwhalProperty()
                        {
                            Id = propGuid,
                            NarwhalNodeId = nodeId,
                            IsConfigForDockerProvider = prop.IsConfig, // Burayý gerektiði gibi ayarlayabilirsin
                            IsNestedProperty = false,
                            IsRequired = false,
                            PropertyDefinition = "", // Gerekirse doldur
                            PropertyName = prop.Name,
                            PropertyRuntimeValueType = prop.Type.Name,
                            ParentPropertyId = null // Parent ID, null olabilir
                        });
                    }
                }

                _dbContext.SaveChanges();

            }




            return true;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public class CompileModel
        {
            public Guid UserId { get; set; }
            public string RepositoryName { get; set; }
            public List<NodeModel> Nodes { get; set; }
            public List<EdgeModel> Edges { get; set; }

        }

        public class NodeModel
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string Type { get; set; }
            public string Data { get; set; }
            public string Style { get; set; }
            public Guid ColregNodeId { get; set; }
            public string? SourceDataValue { get; set; }
        }

        public class EdgeModel
        {
            public string Id { get; set; } //reacthflow__edge-sourceid-sourcehandle-targetid-targethandle
            public string SourceId { get; set; }
            public string SourceHandle { get; set; }
            public string TargetId { get; set; }
            public string TargetHandle { get; set; }
        }

        public static List<string> TopologicalSort(List<string> nodeIds, List<EdgeModel> edges)
        {
            // Ýlgili node'larýn giriþ derecelerini tutacak bir dictionary
            Dictionary<string, int> inDegree = nodeIds.ToDictionary(node => node, node => 0);

            // Her node'dan çýkýþ yapan diðer node'larý tutan adjacency list
            Dictionary<string, List<string>> adjacencyList = nodeIds.ToDictionary(node => node, node => new List<string>());

            // Giriþ derecelerini ve adjacency list'i doldur
            foreach (var edge in edges)
            {
                adjacencyList[edge.SourceId].Add(edge.TargetId);
                inDegree[edge.TargetId]++;
            }

            // Giriþ derecesi 0 olan node'larý kuyruk (queue) içerisine koy
            Queue<string> queue = new Queue<string>();
            foreach (var node in nodeIds)
            {
                if (inDegree[node] == 0)
                {
                    queue.Enqueue(node);
                }
            }

            // Topolojik sýralama sonucu
            List<string> topologicalOrder = new List<string>();

            // Kuyruktaki node'lar üzerinde iþlem yap
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                topologicalOrder.Add(currentNode);

                // Komþularýn giriþ derecelerini azalt
                foreach (var neighbor in adjacencyList[currentNode])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // Eðer topolojik sýralanan node sayýsý, orijinal node sayýsýna eþit deðilse bir döngü vardýr
            if (topologicalOrder.Count != nodeIds.Count)
            {
                throw new Exception("Döngü tespit edildi! Topolojik sýralama yapýlamýyor.");
            }

            return topologicalOrder;
        }

        public enum Procedures
        {
            AddStringDefinition,
            StartNameSpace,
            StartClass,
            AddDockerProvider,
            DefineDockerItemTemplate,
            //mesela buraya dockerImage vs gelecek bunu halledersin sen db ile senkron olmalý sanki
            EndConstructor,
            EndClass,
            EndNameSpace,
        }
        public class CompilerModelProperty
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private string FirstCharToUpper(string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

        private string AddProcedure(NarwhalNode node, List<EdgeModel> edgeList, string visualNodeId, List<NarwhalProperty> narwhalNodeProperties, Dictionary<string, List<NarwhalProperty>> scope, Dictionary<string, string> constantNodeScopeByNodeId)
        {
            var generatedCode = string.Empty;
            var model = new { VariableIdForNode = $"{visualNodeId.Replace("-", "")}", NodeClassName = node.Name, ModelStringId = node.Name + Guid.NewGuid().ToString(), ActiveProperties = new List<CompilerModelProperty>() };

            var targetHandlesForNode = edgeList.Where(edge => edge.TargetId == visualNodeId).ToList();
            foreach (var targetHandle in targetHandlesForNode)
            {
                var targetHandleNNP = narwhalNodeProperties.FirstOrDefault(nnp => string.Equals(nnp.PropertyName, targetHandle.TargetHandle.Replace("Handle", ""), StringComparison.InvariantCultureIgnoreCase));
                NarwhalProperty? sourceHandleNNP = null;
                string? targetSource = null;
                if (!constantNodeScopeByNodeId.ContainsKey(targetHandle.SourceId))
                {
                    sourceHandleNNP = scope[targetHandle.SourceId].FirstOrDefault(nnp => string.Equals(nnp.PropertyName, targetHandle.SourceHandle.Replace("Handle", ""), StringComparison.InvariantCultureIgnoreCase));
                    targetSource = $"n_{targetHandle.SourceId.Replace("-", "")}";
                }

                model.ActiveProperties.Add(new CompilerModelProperty()
                {
                    Name = targetHandleNNP.PropertyName,
                    Value = constantNodeScopeByNodeId.ContainsKey(targetHandle.SourceId) ? constantNodeScopeByNodeId[targetHandle.SourceId] : $"{targetSource}.{sourceHandleNNP.PropertyName}",
                });
            }

            switch (node.GlobalSupportedLibraryName)
            {
                case "Docker":
                    return Engine.Razor.RunCompile(procedureSource[Procedures.DefineDockerItemTemplate], Guid.NewGuid().ToString(), null, model);
                    break;
                default:
                    break;
            }

            var result = Engine.Razor.RunCompile("", Guid.NewGuid().ToString(), null, model);
            return "";
        }

        private string PrepareCompilerToCompile()
        {
            var generatedCode = string.Empty;
            var procedureList = new List<(string, dynamic)>();
            procedureList.Add((procedureSource[Procedures.StartNameSpace], new { Namespace = "MyCompany.MyApp" }));
            procedureList.Add((procedureSource[Procedures.StartClass], new { ClassName = "MainStack" }));
            procedureList.Add((procedureSource[Procedures.AddDockerProvider], new { Id = "docker" }));

            foreach (var p in procedureList)
            {
                generatedCode += Engine.Razor.RunCompile(p.Item1, Guid.NewGuid().ToString(), null, p.Item2 as object);
            }

            return generatedCode;
        }

        private string PrepareCompilerToStop()
        {
            var generatedCode = string.Empty;
            var procedureList = new List<(string, dynamic)>()
            {
                (procedureSource[Procedures.EndConstructor],new { Empty = "" }),
                (procedureSource[Procedures.EndClass],new { Empty = "" }),
                (procedureSource[Procedures.EndNameSpace],new { Empty = "" }),

            };

            foreach (var p in procedureList)
            {
                generatedCode += Engine.Razor.RunCompile(p.Item1, Guid.NewGuid().ToString(), null, p.Item2 as object);
            }

            return generatedCode;
        }

        private string PrepareConstantNodes(List<NodeModel> nodeList)
        {
            var generatedCode = string.Empty;
            foreach (var node in nodeList)
            {
                switch (node.Type)
                {
                    case "stringNode":
                        generatedCode += Engine.Razor.RunCompile(procedureSource[Procedures.AddStringDefinition], Guid.NewGuid().ToString(), null, new { StringId = $"n_{node.Id.ToString().Replace("-", "")}", Value = node.SourceDataValue });
                        break;
                    default:
                        break;
                }
            }
            return generatedCode;
        }

        [HttpPost("Compile")]
        public string CompileUIModel(CompileModel uiModel)
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var token = authHeader.Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Token'dan custom claim deðerlerini al
            var userId = jsonToken?.Claims.First(claim => claim.Type == "unique_name")?.Value;

            var nodeList = uiModel.Nodes.ToList();
            var edgeList = uiModel.Edges.ToList();

            var constantNodeScopeByNodeId = new Dictionary<string, string>();
            var constantNodes = nodeList.Where(node => node.SourceDataValue != null).ToList();
            foreach (var node in constantNodes)
            {
                constantNodeScopeByNodeId.Add(node.Id.ToString(), $"n_{node.Id.ToString().Replace("-", "")}");
            }


            var result = TopologicalSort(nodeList.Select(f => f.Id.ToString()).ToList(), edgeList);
            var propertyScopeByNodeId = new Dictionary<string, List<NarwhalProperty>>();
            var codeResult = string.Empty;

            codeResult += PrepareCompilerToCompile();

            codeResult += PrepareConstantNodes(constantNodes);

            foreach (var nodeId in result)
            {
                if (constantNodeScopeByNodeId.ContainsKey(nodeId))
                {
                    continue;
                }
                var narwhalNode = _dbContext.Nodes.ToList().FirstOrDefault(n => n.Id == nodeList.FirstOrDefault(y => y.Id == Guid.Parse(nodeId)).ColregNodeId);
                var narwhalNodeProperties = _dbContext.Properties.Where(p => p.NarwhalNodeId == narwhalNode.Id).ToList();
                propertyScopeByNodeId.Add(nodeId, narwhalNodeProperties);
                codeResult += AddProcedure(narwhalNode, edgeList, nodeId, narwhalNodeProperties, propertyScopeByNodeId, constantNodeScopeByNodeId);
            }

            codeResult += PrepareCompilerToStop();

            var repo = _dbContext.Repository.FirstOrDefault(f => f.UserId == Guid.Parse(userId));
            if (repo != null) 
            {
                repo.Name = uiModel.RepositoryName;
                repo.ByteCode = codeResult;
            }
            else
            {
                _dbContext.Repository.Add(new Repository()
                {
                    UserId = Guid.Parse(userId),
                    Id = Guid.NewGuid(),
                    Name = $"Narwhal-{uiModel.RepositoryName}",
                    Description = $"Description of {uiModel.RepositoryName}",
                    ForkCount = 0,
                    IsPublic = true,
                    StarCount = 0,
                    ByteCode = codeResult,
                });
            }

            
            _dbContext.SaveChanges();

            return codeResult;
        }

        [HttpGet("GetRepositories")]
        public List<Repository> GetRepositories()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var token = authHeader.Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Token'dan custom claim deðerlerini al
            var userId = jsonToken?.Claims.First(claim => claim.Type == "unique_name")?.Value;

            var repoList = _dbContext.Repository.Where(f=>f.UserId == Guid.Parse(userId)).ToList();
            
            return repoList;
        }

        [HttpGet("AvailableNodes")]
        public List<AvailableNodeModel> AvailableNodes()
        {
            var nodeList = new List<AvailableNodeModel>()
            {
                new AvailableNodeModel
                {
                       Name = "AWS-EC2-INSTANCE",
                           Type = "awsInstanceNode",
                            Id = Guid.NewGuid(),
                },
                   new AvailableNodeModel
                        {
                            Name = "AWS-Auth",
                            Type = "awsAuthNode",
                            Id = Guid.NewGuid(),
                        },
                   new AvailableNodeModel
                        {
                            Name = "AWS-VPC",
                            Type = "awsVpcNode",
                            Id = Guid.NewGuid(),
                        },
                   new AvailableNodeModel
                        {
                            Name = "AWS-VOLUME",
                            Type = "awsVolumeNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_config",
                            Type = "dockerConfigNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_container",
                            Type = "dockerContainerNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_image",
                            Type = "dockerImageNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_network",
                            Type = "dockerNetworkNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_plugin",
                            Type = "dockerPluginNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_registry_image",
                            Type = "dockerRegistryImageNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_secret",
                            Type = "dockerSecretNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_service",
                            Type = "dockerServiceNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_tag",
                            Type = "dockerTagNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "docker_volume",
                            Type = "dockerVolumeNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "string_node",
                            Type = "stringNode",
                            Id = Guid.NewGuid(),
                        },
                        new AvailableNodeModel
                        {
                            Name = "config_node",
                            Type = "configNode",
                            Id = Guid.NewGuid()
                        },
                        new AvailableNodeModel
                        {
                            Name = "ofproperty_node",
                            Type = "ofProperty",
                            Id = Guid.NewGuid()
                        }
            };

            //var dbNodes = _dbContext.Nodes.ToList().Where(f => nodeList.Any(y => y.Name == f.VisualName)).ToList();
            //foreach (var node in nodeList)
            //{
            //    var colregId = dbNodes.FirstOrDefault(f => f.VisualName == node.Name);
            //    if (colregId != null)
            //    {
            //        node.ColregNodeId = colregId.Id;
            //    }
            //}

            return nodeList;
        }

        [HttpPost("Login")]
        public string Login(LoginModel loginModel)
        {
            var user = _dbContext.Users.FirstOrDefault(f => f.Email.ToLower() == loginModel.Email.ToLower() && f.Password == loginModel.Password);
            if (user == null)
            {
                return string.Empty;
            }

            //create a jwt token use user id as claim
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes("256a6994-65ab-477f-9824-23d6e832bc34");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
