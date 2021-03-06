import React from "react";
import "./style.scss";
import { Layout } from "./components";
import { Learn, Progress, Main, Word } from "./pages";
import { Route, Switch } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <Layout >
        <Switch>
          <Route path="/learn" exact component={Learn} />
          <Route path="/learn/:id" component={Word} />
          <Route path="/progress" component={Progress} />
          <Route path="/" component={Main} />
        </Switch>  
      </Layout>
    </div>
  );
}

export default App;
