import React from "react";
import { ReactComponent as Logo } from "../../../../assets/icons/500.svg";
import styles from "./Error500.module.scss";

function Error500() {
  return (
    <div className={styles.root}>
      <Logo fill="#1E88E5" /> <br />
      <h1>500 Error</h1>
      <h2>
        We are facing internal server error <br />and working towards to fix it soon.
      </h2>
    </div>
  );
}

export default Error500;