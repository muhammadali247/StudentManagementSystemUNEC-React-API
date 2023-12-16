import styles from "./TeacherCreateForm.module.scss";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import adminRoutes from "../../../routing/adminRoutes";
import { findPathByLabel } from "../../../utils/routingUtils";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import {
  useTheme,
  Box,
  Button,
  TextField,
  Paper,
  Typography,
  InputLabel,
  Input,
  FormHelperText,
  MenuItem,
  FormControlLabel,
  Checkbox,
} from "@mui/material";
import { Formik } from "formik";
import Header from "../../../components/Admin/Header/Header";
import axiosInstance from "../../../utils/axiosInstance";
import { useSnackbar } from "notistack";
import Autocomplete from "@mui/material/Autocomplete";
import Chip from "@mui/material/Chip";
// import { EventCreateSchema } from "@/validations/FormSchemas";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import useService from "../../../hooks";
import { SUB_GROUPS, EDUCATION_STATUSES, GENDERS, COUNTRIES } from "../../../constants/Enums";
import { jwtDecode } from "jwt-decode";
import AuthProvider from "../../../components/Auth/AuthProvider/AuthProvider";
import { TokenContext } from "../../../Contexts/Token-context";
import { useContext } from "react";

const initialValues = {
  image: null, // Cover Image URL
  appUserId: null,
  name: "", // Event Title
  surname: "", // Event Description
  middleName: "", // Event Description
  admissionYear: null, // Event Date
  birthDate: null, // Event Date
  gender: "", // Event Location
  country: "", // Event Location
};

function TeacherCreateForm() {
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [serverErrors, setServerErrors] = useState({});
  const [users, setUsers] = useState([]);
  const { userServices } = useService();
  const { teacherServices } = useService();

  const token = localStorage.getItem("accessToken");

if (token) {
  // Use the token in your code
  console.log("Token:", token);
} else {
  // Handle the case where the token is not found
  console.error("Token not found in localStorage");
}

  if (token) {
    const decodedToken = jwtDecode(token);
  }
  
  useEffect(() => {
    async function fetchData() {
      try {
        const response = await userServices.getUnassignedUsers(token);
        console.log(response);
        setUsers(response.data);
      } catch (error) {
        console.error("Error fetching users:", error);
      }
    }

    fetchData();
  }, [token]);

  const handleFormSubmit = async (values, { resetForm }) => {

    const formData = new FormData();
    // Append all form fields to the FormData object
    formData.append("image", values.image);
    // formData.append("appUserId", values.appUserId);
    formData.append("appUserId", values.appUserId.id);
    formData.append("name", values.name);
    formData.append("surname", values.name);
    formData.append("middleName", values.middleName);

    if (values.birthDate !== null) {
        formData.append("birthDate", values.birthDate.toISOString());
    }

    formData.append("gender", values.gender);
    formData.append("country", values.country);

    for (const entry of formData.entries()) {
      console.log(entry);
    }

      try {
        const response = await teacherServices.createTeacher(formData, token);
        console.log("Success:", response.data);
        enqueueSnackbar("Teacher added successfully!", {
          variant: "success",
          autoHideDuration: 5000,
        });

        // Reset the form after successful submission
        resetForm();
        
        navigate(findPathByLabel(adminRoutes, "Teachers"));
      } catch (error) {
        if (error.response) {
            if (error.response.status === 500) {
            navigate("/error500");
            } else if (error.response.data && error.response.data.errors) {
            const errorsFromServer = error.response.data.errors;
            setServerErrors(errorsFromServer);
            enqueueSnackbar("Please correct the highlighted errors.", {
                variant: "error",
            });
            }
        } else {
            enqueueSnackbar("Error adding teacher", { variant: "error" });
        }
    }

  };

    return (
    <Box m="20px">
      <Header title="Teachers" subtitle="Create New Record" />
      <Formik
        initialValues={initialValues}
        // validationSchema={EventCreateSchema}

        // onSubmit={handleFormSubmit}
        onSubmit={(values, { resetForm }) => handleFormSubmit(values, { resetForm })}
      >
        {({
          values,
          errors,
          touched,
          handleBlur,
          handleChange,
          handleSubmit,
          setFieldValue,
          setFieldTouched,
        }) => (
          <form onSubmit={handleSubmit}>
            <Box
              display="flex"
              flexDirection="column"
              gap="1rem"
              width="90%"
              px="2rem"
              py="1.5rem"
              borderRadius="12px"
              border={`1px solid ${theme.palette.divider}`}
              bgcolor={colors.primary[400]}
              boxShadow={3}
            >
              <Box>
                {Object.keys(serverErrors).length > 0 && (
                  <Box
                    bgcolor="error.main"
                    color="white"
                    p={2}
                    borderRadius={2}
                  >
                    <Typography variant="h5">Errors:</Typography>
                    <ul>
                      {Object.entries(serverErrors).map(([field, errors]) => (
                        <li key={field}>
                          {errors.map((error, index) => (
                            <div key={index} className={styles.serverError}>
                              {error}
                            </div>
                          ))}
                        </li>
                      ))}
                    </ul>
                  </Box>
                )}
              </Box>

              <Autocomplete
                    PaperComponent={({ children, ...other }) => (
                    <Paper
                        {...other}
                        sx={{ backgroundColor: colors.blueAccent[700] }}
                    >
                        {children}
                    </Paper>
                    )}
                    // multiple
                    id="appUserId"
                    // options={roles}
                    options={users}
                    getOptionLabel={(user) => (user ? user.userName : "")}

                    value={values.appUserId || ''}
                    // value={values.appUserId.id || null}
                    onChange={(_, newValue) => {
                    setFieldValue("appUserId", newValue);
                    setFieldTouched("appUserId", true); // Set the touch status
                    }}
                    onBlur={() => setFieldTouched("appUserId", true)} // Ensure that field is marked touched on blur.
                    renderInput={(params) => (
                    <TextField
                        {...params}
                        variant="filled"
                        label="Select User for Teacher"
                        error={!!touched.appUserId && !!errors.appUserId}
                        helperText={touched.appUserId && errors.appUserId}
                        InputLabelProps={{
                        style: { fontSize: "1rem" },
                        focused: false,
                        }}
                        FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                    />
                    )}
                    renderTags={(selectedUsers, getTagProps) =>
                        selectedUsers.map((specRole, index) => (
                        <Chip
                        key={specRole.id} // using the unique project ID as the key
                        variant="outlined"
                        label={specRole.name} // displaying the project name on the chip
                        {...getTagProps({ index })}
                        />
                    ))
                    }
                />

              <Box className={styles.InputRow}>
                <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Teacher name"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.name}
                    name="name"
                    error={!!touched.name && !!errors.name}
                    helperText={touched.name && errors.name}
                    // Changes for increasing font size of label and helperText
                    InputLabelProps={{
                    style: { fontSize: "1rem" },
                    focused: false,
                    }} // Increase font size of label
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                />

                <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Teacher surname"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.surname}
                    name="surname"
                    error={!!touched.surname && !!errors.surname}
                    helperText={touched.surname && errors.surname}
                    // Changes for increasing font size of label and helperText
                    InputLabelProps={{
                    style: { fontSize: "1rem" },
                    focused: false,
                    }} // Increase font size of label
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                />
              </Box>

              <Box className={styles.InputRow}>
              <div
                    style={{
                    border: "1px solid #ccc",
                    borderRadius: "8px",
                    padding: "8px",
                    display: "inline-block",
                    }}
                >
                    <InputLabel htmlFor="image">Image</InputLabel>
                    <Input
                    type="file"
                    id="image"
                    name="image"
                    onBlur={handleBlur}
                    onChange={(event) => {
                        setFieldValue(
                        "image",
                        event.currentTarget.files[0]
                        );
                    }}
                    error={!!touched.image && !!errors.image}
                    inputProps={{ accept: "image/*" }}
                    />
                </div>

                <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Teacher middlename"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.middleName}
                    name="middleName"
                    error={!!touched.middleName && !!errors.middleName}
                    helperText={touched.middleName && errors.middleName}
                    // Changes for increasing font size of label and helperText
                    InputLabelProps={{
                    style: { fontSize: "1rem" },
                    focused: false,
                    }} // Increase font size of label
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                />
              </Box>

              <Box className={styles.InputRow}>
                <DateTimePicker
                  // disablePast
                  label="Birth Date"
                  value={values.birthDate}
                  onBlur={() => setFieldTouched("birthDate", true)}
                  onChange={(date) => setFieldValue("birthDate", date)}
                  slotProps={{
                    textField: {
                      fullWidth: true,
                      variant: "filled",
                      error:
                        Boolean(touched.birthDate) && Boolean(errors.birthDate),
                      helperText:
                        touched.birthDate && errors.birthDate
                          ? errors.birthDate
                          : "\u00a0",
                      InputLabelProps: {
                        style: { fontSize: "1rem" },
                        focused: false,
                      },
                      FormHelperTextProps: { style: { fontSize: "0.9rem" } },
                    },
                  }}
                />
            </Box>

              <Box className={styles.InputRow}>
              <TextField
                    fullWidth
                    variant="filled"
                    select
                    label="Genders"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.gender}
                    name="gender"
                    error={!!touched.gender && !!errors.gender}
                    helperText={
                      touched.gender && errors.gender
                        ? errors.gender
                        : "\u00a0"
                    }
                    InputLabelProps={{
                      style: { fontSize: "1rem" },
                      focused: false,
                    }}
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
                    SelectProps={{
                      MenuProps: {
                        PaperProps: {
                          style: {
                            backgroundColor: colors.blueAccent[700],
                          },
                        },
                      },
                    }}
                >
                    {GENDERS.map((status) => (
                      <MenuItem key={status} value={status}>
                        {status}
                      </MenuItem>
                    ))}
                </TextField>

                <TextField
                    fullWidth
                    variant="filled"
                    select
                    label="Origin Country"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.country}
                    name="country"
                    error={!!touched.country && !!errors.country}
                    helperText={
                      touched.country && errors.country
                        ? errors.country
                        : "\u00a0"
                    }
                    InputLabelProps={{
                      style: { fontSize: "1rem" },
                      focused: false,
                    }}
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
                    SelectProps={{
                      MenuProps: {
                        PaperProps: {
                          style: {
                            backgroundColor: colors.blueAccent[700],
                          },
                        },
                      },
                    }}
                >
                    {COUNTRIES.map((status) => (
                      <MenuItem key={status} value={status}>
                        {status}
                      </MenuItem>
                    ))}
                </TextField>
              </Box>

              <Box className={styles.InputRow}>
                {/* Submit Button */}
                <Button
                  fullWidth
                  type="submit"
                  variant="contained"
                  style={{
                    backgroundColor: colors.blueAccent[400],
                    fontSize: "0.8rem",
                    fontWeight: "bold",
                  }}
                >
                  Submit
                </Button>

                {/* Cancel Button */}
                <Button
                  fullWidth
                  variant="contained"
                  style={{
                    backgroundColor: colors.grey[500],
                    fontSize: "0.8rem",
                    fontWeight: "bold",
                  }}
                //   onClick={() => navigate(-1)} // navigating back to the previous page
                onClick={() => navigate(findPathByLabel(adminRoutes, "Teachers"))} // navigating back to the previous page
                >
                  Cancel
                </Button>
              </Box>
            </Box>
          </form>
        )}
      </Formik>
    </Box>
  );
}

export default TeacherCreateForm;