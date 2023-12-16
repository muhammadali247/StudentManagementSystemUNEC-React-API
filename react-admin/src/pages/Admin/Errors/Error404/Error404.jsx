import React from "react";
import { ReactComponent as Logo } from "../../../../assets/icons/404.svg";
import styles from "./Error404.module.scss";

function Error404() {
  return (
    <div className={styles.root}>
      <Logo fill="#1E88E5" /> <br/ >
      <h1>404 Error</h1>
      <h2>We can&apos;t find the page that you are looking for.</h2>
    </div>
  );
}

export default Error404;