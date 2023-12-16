import styles from "./ResetPassword.module.scss";
import React, { useState, useEffect } from "react";
import {
  Checkbox,
  useTheme,
  TextField,
  Card,
  Button,
  Grid,
  Box,
  IconButton,
  Typography,
  FormControlLabel,
} from "@mui/material";
import { Form, Formik, useField, Field } from "formik";
import { ReactComponent as Logo } from "../../../assets/icons/reset-password.svg";
import { ReactComponent as Logo2 } from "../../../assets/icons/403.svg";
import { ResetPasswordSchema } from "../../../validations/FormSchemas";
import authRoutes from "../../../routing/authRoutes";
import axios from "axios";
// import {
//   ACCOUNT_RESET_PASSWORD_URL,
//   ACCOUNT_VALIDATE_RESET_PASSWORD_TOKEN_URL,
// } from "@/constants/Urls";
import useService from "../../../hooks";
import { useNavigate, useLocation } from "react-router-dom";
import { useSnackbar } from "notistack";
import { findPathByLabel } from "../../../utils/routingUtils";
import LoadingIndicator from '../../../components/Global/UI/LoadingIndicator';

const AppTextField = (props) => {
  const [field, meta] = useField(props);
  const errorText = meta.error && meta.touched ? meta.error : " ";
  return (
    <TextField
      {...props}
      {...field}
      helperText={errorText}
      error={meta.touched && !!meta.error}
      sx={{
        width: "100%",
        mb: 1,
      }}
    />
  );
};

function ResetPassword() {
  const location = useLocation();
  const urlParams = new URLSearchParams(location.search);
  const email = urlParams.get("email");
  const token = urlParams.get("token");
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const theme = useTheme();
  const [serverErrors, setServerErrors] = React.useState({});
  const [isTokenValid, setIsTokenValid] = useState(null);
  const { authServices } = useService();

  const initialValues = {
    email: email || "",
    token: token || "",
    newPassword: "",
    confirmPassword: "",
  };

  // useEffect(() => {
  //   // Function to validate token
  //   const validateToken = async () => {
  //     try {
  //       // const response = await authServices.validateResetPassword(params: {email,token});
  //       // const response = await authServices.validateResetPassword(email,token);
  //       const response = await authServices.validateResetPassword({
  //         params: {
  //           email,
  //           token,
  //         },
  //       });

  //       if (response.status === 200) {
  //         setIsTokenValid(true);
  //       } else {
  //         setIsTokenValid(false);
  //       }
  //     } catch (error) {
  //       console.log(error);
  //       setIsTokenValid(false);
  //     }
  //   };
    

  //   // Call the validate token function
  //   validateToken();
  // }, [email, token]); // Run this effect when the component mounts and whenever email or token changes

  useEffect(() => {
    // Function to validate token
    const validateToken = async () => {
      try {
        const response = await authServices.validateResetPassword({
          email,
          token,
        });
  
        if (response.status === 200) {
          setIsTokenValid(true);
        } else {
          setIsTokenValid(false);
        }
      } catch (error) {
        console.log(error);
        setIsTokenValid(false);
      }
    };
  
    // Call the validate token function
    validateToken();
  }, [email, token]);
  

  const handleFormSubmit = async (values, { setSubmitting, setErrors }) => {
    setSubmitting(true);

    const data = {
      email: values.email,
      token: values.token,
      newPassword: values.newPassword,
      confirmPassword: values.confirmPassword,
    };

    try {
      const response = await authServices.ResetPassword(data);

      console.log("Password reset successful:", response.data);
      enqueueSnackbar("Password has been reset successfully!", {
       variant: "success",
       autoHideDuration: 5000,
       });
      navigate(findPathByLabel(authRoutes, "SignIn"));

    } catch (error) {
      let errorMessage = "An error occurred while resetting the password.";
        if (error.response) {
          if (error.response.status === 500) {
            navigate("/error500");
            return;
          } else if (error.response.data && error.response.data.errors) {
            const errorsFromServer = error.response.data.errors;
            setErrors(errorsFromServer);
            setServerErrors(errorsFromServer); // Store the server errors
            console.log(errorsFromServer);
            errorMessage = "Please correct the highlighted errors.";
          }
        }
        enqueueSnackbar(errorMessage, {
          variant: "error",
        });
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div>
      {isTokenValid === null ? (
        <LoadingIndicator />
        ) : isTokenValid ? (
        <Box
          sx={{
            padding: "1rem",
            display: "flex",
            minHeight: "100vh",
            flex: 1,
            width: "100%",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <Card
            sx={{
              maxWidth: 1024,
              borderRadius: "2%",
              width: "100%",
              backgroundColor: "#1F2A40",
              paddingX: 8,
              paddingY: 3,
              paddingLeft: { xs: 8, md: 2 },
              overflow: "hidden",
              boxShadow:
                "0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)",
            }}
          >
            <Grid
              container
              spacing={5}
              sx={{
                alignItems: { md: "center" },
              }}
            >
              <Grid
                item
                xs={12}
                md={6}
                sx={{
                  textAlign: "center",
                }}
              >
                <Logo fill={theme.palette.primary.main} />
              </Grid>
              <Grid
                item
                xs={12}
                md={6}
                sx={{
                  textAlign: "center",
                }}
              >
                <Box
                  sx={{
                    mb: { xs: 6, xl: 3 },
                    fontWeight: 700,
                    fontSize: 30,
                  }}
                >
                  Sign In
                </Box>
                {Object.keys(serverErrors).length > 0 && (
                  <Box
                    bgcolor="error.main"
                    color="white"
                    p={2}
                    borderRadius={2}
                    sx={{
                      mb: { xs: 6, xl: 3 },
                    }}
                  >
                    <Typography variant="h5">Errors:</Typography>
                    <ul>
                      {Object.entries(serverErrors).map(([field, errors]) => (
                        <li key={field}>
                          {errors.map((error, index) => (
                            <div key={index}>{error}</div>
                          ))}
                        </li>
                      ))}
                    </ul>
                  </Box>
                )}
                <Formik
                  validateOnChange={true}
                  initialValues={initialValues}
                  validationSchema={ResetPasswordSchema}
                  onSubmit={handleFormSubmit}
                >
                  {({ isSubmitting, values, handleChange }) => (
                    <Form noValidate autoComplete="on">
                      <Grid container spacing={1}>
                        <Grid item xs={12}>
                          <AppTextField
                            label="New Password"
                            name="newPassword"
                            type="password"
                            variant="outlined"
                            autoComplete="current-password"
                          />
                        </Grid>
                        <Grid item xs={12}>
                          <AppTextField
                            label="Retype Password"
                            name="confirmPassword"
                            type="password"
                            variant="outlined"
                          />
                        </Grid>
                      </Grid>
                      <Button
                        variant="contained"
                        color="primary"
                        disabled={isSubmitting}
                        sx={{
                          width: "100%",
                          height: 44,
                        }}
                        type="submit"
                      >
                        Reset My Password
                      </Button>
                    </Form>
                  )}
                </Formik>
              </Grid>
            </Grid>
          </Card>
        </Box>
      ) : (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <Grid
          container
          spacing={5}
          sx={{
            alignItems: 'center',
            justifyContent: 'center',
            maxWidth: '1000px',  // Set the max-width you want
          }}
        >
          <Grid
            item
            xs={12}
            md={6}
            sx={{
              textAlign: 'center',
            }}
          >
            <Logo2 fill={theme.palette.primary.main} />
          </Grid>
          <Grid item xs={12} md={12}>
            <div style={{ textAlign: 'center', fontSize: "2rem" }}>
              Oops, the page is no longer accessible, the token was invalid.
            </div>
          </Grid>
        </Grid>
      </div>
      )}
    </div>
  );
}

export default ResetPassword;


