import { useState, useEffect } from "react";
import "./App.css";


function Todo({ title, completed, onCheck, onRemove }) {
  // const [todos, setTodos] = useState([]);

  const onChange = (event) => {
    onCheck(event.target.checked);
  };

  return (
    <>
      <span>{title}</span>
      <input type="checkbox" checked={completed} onChange={onChange} />
      <button onClick={onRemove}>Remove</button>
    </>
  );
}


// function CreateTodoForm({ onCreate }) {
//   const [newTitle, setNewTitle] = useState("");
//   //const [title, setTitle] = useState([]);
//   // console.log(onCreate);

//   return (
//     <>
//       <input value={newTitle} onChange={(event) => setNewTitle(event.target.value)} />
//       <button
//         onClick={() => {
//         onCreate(newTitle);
//           //setTitle("");
//         }}
//       >
//         Add
//       </button>
//     </>
//   );
// }

async function fetchData() {
  let result = await fetch("http://localhost:5271/todos/");
  return await result.json()
}


function App() {
  const [todos, setTodos] = useState([]);

  // useEffect(() => {
  //   fetch("http://localhost:5271/")
  //     .then((res) => res.json())
  //     .then((res) => setTodos(res));
      
  //     //console.log(res);
  //     console.log(todos);
  // }, []);
  //     console.log(todos);
  useEffect(() => {
    fetchData().then((result => setTodos(result)));
  }, []);
  console.log(todos)

  
  // useEffect(() => {
  //   fetch("http://localhost:5721/todos/").then(res => res.json()).then(setTodos);
  // }, [])
          
  // const createTodo = (title) => {
  //   fetch(`http://localhost:5271/todos/?title=${title}`, {   
  //     method: "POST",
  //     headers: {
  //       "Content-Type": "application/json",
  //     },
      
  //   })
  //     .then((res) => res.json())
  //     .then((todo) => {
  //       setTodos([...todos, todo]);
  //     });
  //     console.log(todos)
  // };

  // const updateTodo = (todo, completed) => {
  //   fetch(`http://localhost:5271/todo/${todo.id}?completed=true`, {
  //     method: "PUT",
  //     headers: { "Content-Type": "application/json"},
  //   }).then(() => {
      
  //     setTodos(
  //       todos.map((all) => (all === todo ? { ...todo, completed } : all))
  //     );
  //   });
  // };


  // const removeTodo = (todo) => {
  //   fetch(`http://localhost:5271/todo/${todo.id}`,
  //    { method: "DELETE" }).then(
  //     () => {
  //       setTodos(todos.filter((all) => all !== todo));
  //     }
  //   );
  // };

  todos.sort((a, b) => (a.completed ? 1 : -1));

  return (
    <div className="App">
       {/* <CreateTodoForm  onCreate={createTodo}/> */}
        {todos.length === 0 ? (
          <p>loading...</p>
        ):
        (
        <ul>
        {todos.map((todo) => (
          <li key={todo.id}>
            <Todo
              title={todo.title}
              completed={todo.completed}
              // onCheck={(completed) => {
              //   updateTodo(todo, completed);
              // }}
              // onRemove={() => removeTodo(todo)}
            />
          </li>
        ))}
      </ul>
      )}
        

    </div>
  );
}

export default App;