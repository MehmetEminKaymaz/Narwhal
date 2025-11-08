import TemplatePointers from "./components/TemplatePointers"
import logo from "../../narwhallogo2.jpg"



function LandingIntro(){

    return(
        <div className="hero min-h-full rounded-l-xl bg-base-200">
            <div className="hero-content py-12">
              <div className="max-w-md">

              <h1 className='text-3xl text-center font-bold '><img src={logo} className="w-12 inline-block mr-2 mask mask-circle" alt="dashwind-logo" />Narwhal Language Compiler Platform</h1>

               
              
              {/* Importing pointers component */}
              <TemplatePointers />
              
              </div>

            </div>
          </div>
    )
      
  }
  
  export default LandingIntro