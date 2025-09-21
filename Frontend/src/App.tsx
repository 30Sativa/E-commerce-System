import { useState } from "react";
import "./App.css";

function App() {
  const[jobs, setJobs]  = useState<[]>([]);

  return (
    <div className="bg-clip-padding 32">
      <input/>
      <button>Add</button>

      <ul>
        <li></li>
      </ul>
    </div>
  )
    
}

export default App;
