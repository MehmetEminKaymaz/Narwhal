import logo from '../../../narwhallogo2.jpg';

function TemplatePointers(){
    return(
        <>
         <img src={logo} alt="Avatar" />
         <h1 className="text-2xl mt-8 font-bold">Welcome to Narwhal Programming Language</h1>
          <p className="py-2 mt-4">✓ <span className="font-semibold">Fastest</span> development</p>
          <p className="py-2 ">✓ <span className="font-semibold">C#</span> output</p>
          <p className="py-2">✓ <span className="font-semibold">HashiCorp Configuration Language </span> output</p>
          <p className="py-2  ">✓ User-friendly <span className="font-semibold">documentation</span></p>
          <p className="py-2  mb-4">✓ <span className="font-semibold">Docker Support</span></p>
        </>
    )
}

export default TemplatePointers