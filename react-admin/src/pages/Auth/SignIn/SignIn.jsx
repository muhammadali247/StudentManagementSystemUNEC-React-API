import styles from "./SignIn.module.scss";
import React from "react";
import GoogleIcon from "@mui/icons-material/Google";
import TwitterIcon from "@mui/icons-material/Twitter";
import FacebookIcon from "@mui/icons-material/Facebook";
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
import { ReactComponent as Logo } from "../../../assets/icons/login.svg";
import { SignInSchema } from "../../../validations/FormSchemas";
import authRoutes from "../../../routing/authRoutes";
import adminRoutes from "../../../routing/adminRoutes";
import axios from "axios";
// import { ACCOUNT_LOGIN_URL, ACCOUNT_RENEW_TOKEN_URL } from "@/constants/Urls";
import useService from "../../../hooks";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "notistack";
import { findPathByLabel } from "../../../utils/routingUtils";
import { jwtDecode } from 'jwt-decode';
import useAuth from '../../../utils/useAuth';
import { tokenRoleProperty } from "../../../components/Auth/AuthProvider/AuthProvider";

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

const initialValues = {
  usernameOrEmail: "",
  password: "",
  rememberMe: false,
};

function SignIn() {
  const { updateUser } = useAuth();
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const theme = useTheme();
  const [serverErrors, setServerErrors] = React.useState({});
  const { authServices } = useService();


  const handleFormSubmit = async (values, { setSubmitting, setErrors, resetForm }) => {
    setSubmitting(true);
  
    try {
      // console.log(values);
      const response = await authServices.Login({
        UserNameOrEmail: values.usernameOrEmail,
        Password: values.password,
        RememberMe: values.rememberMe,
      });
  
      console.log("Login successful:", response.data);
      console.log("Full API Response:", response);
      enqueueSnackbar("Logged in successfully!", {
        variant: "success",
        autoHideDuration: 1500,
      });
  
      // Store the received token and expiration time in local storage
      localStorage.setItem("accessToken", response.data.accessToken);
      const decodedToken = jwtDecode(response.data.accessToken);
      updateUser(decodedToken);
      const expirationTime = decodedToken.exp;
      localStorage.setItem("tokenExpiration", expirationTime);
  
      // Navigate to the dashboard or another authenticated route
      // navigate(-1);

      switch (decodedToken[tokenRoleProperty]) {
        case "Admin":
          console.log("Navigating to Admin Dashboard");
          navigate(findPathByLabel(adminRoutes, "Dashboard"));
          break;
        case "Moderator":
          console.log("Navigating to Admin Dashboard");
          navigate(findPathByLabel(adminRoutes, "Dashboard"));
          break;
        case "Teacher":
          console.log("Navigating to Teacher Dashboard");
          navigate(findPathByLabel(adminRoutes, "Dashboard"));
          break;
        case "Student":
          console.log("Navigating to Student Dashboard");
          navigate(findPathByLabel(adminRoutes, "Dashboard"));
          break;
        default:
          console.log("Unknown role, navigating to a default route");
          navigate('/default-route'); // Specify a default route

      }
      } 
      catch (error) {
      console.log(error);
      let errorMessage = "An error occurred while logging in.";
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
        autoHideDuration: 2000,
      });
    } finally {
      setSubmitting(false);
    }
  };
  

  return (
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
              validationSchema={SignInSchema}
              onSubmit={handleFormSubmit}
            >
              {({ isSubmitting, values, handleChange }) => (
                <Form noValidate autoComplete="on">
                  <Grid container spacing={1}>
                    <Grid item xs={12}>
                      <AppTextField
                        label="Username or Email"
                        name="usernameOrEmail"
                        variant="outlined"
                        autoComplete="username"
                      />
                    </Grid>
                    <Grid item xs={12}>
                      <AppTextField
                        label="Password"
                        name="password"
                        type="password"
                        variant="outlined"
                        autoComplete="current-password"
                      />
                    </Grid>
                  </Grid>
                  <Box
                    sx={{
                      mb: { xs: 5, xl: 2 },
                      display: "flex",
                      flexWrap: "wrap",
                      alignItems: "center",
                      justifyContent: "space-between",
                    }}
                  >
                    <FormControlLabel
                      control={
                        <Field
                          as={Checkbox}
                          type="checkbox"
                          name="rememberMe"
                        />
                      }
                      label="Remember me"
                    />
                    <Box
                      component="span"
                      sx={{
                        cursor: "pointer",
                        color: "primary.main",
                        fontWeight: 700,
                        fontSize: 14,
                      }}
                      onClick={() => navigate(findPathByLabel(authRoutes, "ForgotPassword"))}
                    >
                      Forgot your password?
                    </Box>
                  </Box>
                  <Button
                    variant="contained"
                    color="primary"
                    disabled={isSubmitting}
                    // onClick={() => handleFormSubmit()}
                    sx={{
                      width: "100%",
                      height: 44,
                    }}
                    type="submit"
                  >
                    Sign In
                  </Button>
                </Form>
              )}
            </Formik>

            {/* Social media login buttons and additional text */}
            {/* <Box
              sx={{
                mt: 3,
                mb: 3,
                display: "flex",
                flexDirection: { xs: "column", sm: "row" },
                justifyContent: { sm: "center" },
                alignItems: { sm: "center" },
              }}
            >
              <Box
                component="span"
                sx={{
                  fontSize: 14,
                  mr: 4,
                }}
              >
                Or login with
              </Box>
              <Box display="inline-block">
                <IconButton>
                  <GoogleIcon sx={{ color: "text.primary" }} />
                </IconButton>
                <IconButton>
                  <FacebookIcon sx={{ color: "text.primary" }} />
                </IconButton>
                <IconButton>
                  <TwitterIcon sx={{ color: "text.primary" }} />
                </IconButton>
              </Box>
            </Box> */}
            <Box
              sx={{
                fontSize: 14,
                fontWeight: 700,
                marginTop: 3
              }}
            >
              <Box component="span" sx={{ mr: 2 }}>
                Don&apos;t have an account?
              </Box>
              <Box
                component="span"
                sx={{
                  color: "primary.main",
                  cursor: "pointer",
                }}
                onClick={() => navigate(findPathByLabel(authRoutes, "SignUp"))}
              >
                Sign Up
              </Box>
            </Box>
          </Grid>
        </Grid>
      </Card>
    </Box>
  );
}

export default SignIn;