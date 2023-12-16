import { Children, createContext, useState } from "react";

export const TokenContext = createContext();

export const TokenContextProvider = ({ children }) => {
  const [changeState, setChangeState] = useState();
  <TokenContext.Provider>{children}</TokenContext.Provider>;
};

// export const TokenContextProvider = ({ children }) => {
//   // const [changeState, setChangeState] = useState();
//   const [changeState, setChangeState] = useState();
//   <TokenContext.Provider value={{ token: changeState }}>{children}</TokenContext.Provider>;
// };