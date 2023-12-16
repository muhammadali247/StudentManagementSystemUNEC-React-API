import React from 'react';
import RiseLoader from "react-spinners/RiseLoader";

function LoadingIndicator() {
  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <RiseLoader color="rgba(70, 121, 96, 1)" size={15} />
    </div>
  );
}

export default LoadingIndicator;
