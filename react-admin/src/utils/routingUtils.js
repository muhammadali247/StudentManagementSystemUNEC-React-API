export const findPathByLabel = (routes, label, params = {}) => {
    for (const route of routes) {
      if (route.label === label) {
        return replaceParams(route.path, params);
      }
      
      if (route.children) {
        for (const childRoute of route.children) {
          if (childRoute.label === label) {
            const combinedPath = combinePaths(route.path, childRoute.path);
            return replaceParams(combinedPath, params);
          }
        }
      }
    }
    return null;
  };
  
  const combinePaths = (parentPath, childPath) => {
    const separator = parentPath.endsWith('/') ? '' : '/';
    return `${parentPath}${separator}${childPath}`;
  }
  
  const replaceParams = (path, params) => {
    for (const [key, value] of Object.entries(params)) {
      path = path.replace(`:${key}`, value);
    }
    return path;
  };