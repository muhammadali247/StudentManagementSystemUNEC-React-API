import styles from "./StudentCreateForm.module.scss";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
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
    // appUserId: null,
    name: "", // Event Title
    surname: "", // Event Description
    middleName: "", // Event Description
    admissionYear: null, // Event Date
    birthDate: null, // Event Date
    eventDuration: "", // Event Duration
    educationStatus: "", // Live Stream URL
    corporativeEmail: "", // Event Location
    corporativePassword: "", // Event Location
    gender: "", // Event Location
    country: "", // Event Location
    mainGroup: null,
    groupIds: [], // List of Musical Project IDs
    subGroups: [],
};

function StudentUpdateForm() {
    const { enqueueSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const [musicalProjects, setMusicalProjects] = useState([]);
    const [serverErrors, setServerErrors] = useState({});

    const [existingData, setExistingData] = useState(null);

    const [groups, setGroups] = useState([]);
    const [users, setUsers] = useState([]);
    const { groupServices } = useService();
    const { userServices } = useService();
    const { studentServices } = useService();
    const { studentId } = useParams();
    // const isNonMobile = useMediaQuery("(min-width:600px)");
  
    // const { token } = useContext(TokenContext);
    const token = localStorage.getItem("accessToken");

    // const contentType = serverExpectsJSON ? "application/json" : "multipart/form-data";
  
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

    // Fetch existing data when the component mounts
    useEffect(() => {
      async function fetchExistingData() {
        try {
          const response = await studentServices.getStudentById(studentId, token);
          setExistingData(response.data);
        } catch (error) {
          console.error("Error fetching existing data:", error);
        }
      }

      fetchExistingData();
    }, [studentId, token]);
    
    // useEffect(() => {
    //   async function fetchData() {
    //     try {
    //       const response = await userServices.getUnassignedUsers(token);
    //       console.log(response);
    //       setUsers(response.data);
    //     } catch (error) {
    //       console.error("Error fetching users:", error);
    //     }
    //   }
  
    //   fetchData();
    // }, [token]);
  
    useEffect(() => {
      async function fetchData() {
        try {
          const response = await groupServices.getAllGroups();
          setGroups(response.data);
        } catch (error) {
          console.error("Error fetching roles:", error);
        }
      }
  
      fetchData();
    }, []);

    // Wait until the existing data is fetched
    if (!existingData) {
      return <div>Loading...</div>;
    }

    // Merge existing data with initial values
    const mergedValues = { ...initialValues, ...existingData };
  
    const handleFormSubmit = async (values, { resetForm }) => {
      const formData = new FormData();
      // Append all form fields to the FormData object
      formData.append("image", values.image);
      // formData.append("appUserId", values.appUserId);
      // formData.append("appUserId", values.appUserId.id);
      formData.append("name", values.name);
      formData.append("surname", values.surname);
      formData.append("middleName", values.middleName);
  
      if (values.admissionYear !== null) {
          formData.append("admissionYear", values.admissionYear.toISOString());
      }
      // formData.append("ticketType", values.ticketType);
      formData.append("educationStatus", values.educationStatus);
  
      if (values.birthDate !== null) {
          formData.append("birthDate", values.birthDate.toISOString());
      }
  
      formData.append("corporativeEmail", values.corporativeEmail);
      formData.append("corporativePassword", values.corporativePassword);
      formData.append("gender", values.gender);
      formData.append("country", values.country);
      // formData.append("mainGroup", values.mainGroup);
      formData.append("mainGroup", values.mainGroup.id);
  
      // Append musical project IDs
      values.groupIds.forEach((group) => {
          formData.append("groupIds", group.id);
      });
  
      values.subGroups.forEach((subGroup) => {
          formData.append("subGroups", subGroup);
      });
  
      for (const entry of formData.entries()) {
        console.log(entry);
      }

      console.log("Request Headers:", {
        Authorization: `Bearer ${token}`,
        "Content-Type": "multipart/form-data",
      });
      
  
        try {
          // const response = await studentServices.updateStudent(studentId, formData, token, {
          //   headers: {
          //     Authorization: `Bearer ${token}`,
          //     "Content-Type": "multipart/form-data", // Update the Content-Type here
          //   },
          // });
          const response = await studentServices.updateStudent(studentId, formData, token);
          console.log("Success:", response.data);
          enqueueSnackbar("Student updated successfully!", {
            variant: "success",
            autoHideDuration: 5000,
          });
  
          // Reset the form after successful submission
          resetForm();
          
          navigate(findPathByLabel(adminRoutes, "Students"));
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
              enqueueSnackbar("Error adding student", { variant: "error" });
          }
      }
  
    };
  
      return (
      <Box m="20px">
        <Header title="Students" subtitle="Update existing record" />
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
  
                <Box className={styles.InputRow}>
                  <TextField
                      fullWidth
                      variant="filled"
                      type="text"
                      label="Student name"
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
                      label="Student surname"
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
  
                  <TextField
                      fullWidth
                      variant="filled"
                      type="text"
                      label="Student middlename"
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
                      select
                      label="Education Status"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      value={values.educationStatus}
                      name="educationStatus"
                      error={!!touched.educationStatus && !!errors.educationStatus}
                      helperText={
                        touched.educationStatus && errors.educationStatus
                          ? errors.educationStatus
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
                      {EDUCATION_STATUSES.map((status) => (
                        <MenuItem key={status} value={status}>
                          {status}
                        </MenuItem>
                      ))}
                  </TextField>
                </Box>
  
                {/* <Box className={styles.InputRow}>
  
                </Box> */}
  
                <Box className={styles.InputRow}>
                  <DateTimePicker
                    // disablePast
                    label="Admission Year"
                    value={values.admissionYear}
                    onBlur={() => setFieldTouched("admissionYear", true)}
                    onChange={(date) => setFieldValue("admissionYear", date)}
                    slotProps={{
                      textField: {
                        fullWidth: true,
                        variant: "filled",
                        error:
                          Boolean(touched.admissionYear) && Boolean(errors.admissionYear),
                        helperText:
                          touched.admissionYear && errors.admissionYear
                            ? errors.admissionYear
                            : "\u00a0",
                        InputLabelProps: {
                          style: { fontSize: "1rem" },
                          focused: false,
                        },
                        FormHelperTextProps: { style: { fontSize: "0.9rem" } },
                      },
                    }}
                  />
  
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
                    type="text"
                    label="Corporative Email"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.corporativeEmail}
                    name="corporativeEmail"
                    error={!!touched.corporativeEmail && !!errors.corporativeEmail}
                    helperText={touched.corporativeEmail && errors.corporativeEmail}
                    InputLabelProps={{
                      style: { fontSize: "1rem" },
                      focused: false,
                    }}
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
                  />
  
                  <TextField
                    fullWidth
                    variant="filled"
                    type="password"
                    label="corporativePassword"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.corporativePassword}
                    name="corporativePassword"
                    error={!!touched.corporativePassword && !!errors.corporativePassword}
                    helperText={touched.corporativePassword && errors.corporativePassword}
                    InputLabelProps={{
                      style: { fontSize: "1rem" },
                      focused: false,
                    }}
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
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
  
                {/* <Box className={styles.InputRow}>
  
                </Box> */}
  
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
                      id="mainGroup"
                      // options={roles}
                      options={groups}
                      // getOptionLabel={(option) => option.name}
                      // getOptionLabel={(group) => group.name}
                      getOptionLabel={(group) => (group ? group.name : "")}
                      // value={values.mainGroup.id}
                      value={values.mainGroup}
                      onChange={(_, newValue) => {
                      setFieldValue("mainGroup", newValue);
                      setFieldTouched("mainGroup", true); // Set the touch status
                      }}
                      onBlur={() => setFieldTouched("mainGroup", true)} // Ensure that field is marked touched on blur.
                      renderInput={(params) => (
                      <TextField
                          {...params}
                          variant="filled"
                          label="Select main Group for Student"
                          error={!!touched.mainGroup && !!errors.mainGroup}
                          helperText={touched.mainGroup && errors.mainGroup}
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
  
  
                 <Autocomplete
                  PaperComponent={({ children, ...other }) => (
                    <Paper
                      {...other}
                      sx={{ backgroundColor: colors.blueAccent[700] }}
                    >
                      {children}
                    </Paper>
                  )}
                  multiple
                  id="groupIds"
                  options={groups}
                  // getOptionLabel={(group) => group.name}
                  getOptionLabel={(group) => (group ? group.name : "")}
                  value={values.groupIds}
                  onChange={(_, newValue) => {
                    setFieldValue("groupIds", newValue);
                    setFieldTouched("groupIds", true); // Set the touch status
                  }}
                  onBlur={() => setFieldTouched("groupIds", true)} // Ensure that field is marked touched on blur.
                  renderInput={(params) => (
                    <TextField
                      {...params}
                      variant="filled"
                      label="Select Groups for Student"
                      error={!!touched.groupIds && !!errors.groupIds}
                      helperText={touched.groupIds && errors.groupIds}
                      InputLabelProps={{
                        style: { fontSize: "1rem" },
                        focused: false,
                      }}
                      FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                    />
                  )}
                  renderTags={(selectedGroups, getTagProps) =>
                      selectedGroups.map((specRole, index) => (
                      <Chip
                        key={specRole.id} // using the unique project ID as the key
                        variant="outlined"
                        label={specRole.name} // displaying the project name on the chip
                        {...getTagProps({ index })}
                      />
                    ))
                  }
                />
  
                 <Autocomplete
                     multiple
                     id="subGroups"
                     options={SUB_GROUPS}
                     value={values.subGroups}
                     onChange={(_, newValues) => {
                     setFieldValue("subGroups", newValues);
                     setFieldTouched("subGroups", true);
                     }}
                     onBlur={() => setFieldTouched("subGroups", true)}
                     renderInput={(params) => (
                     <TextField
                         {...params}
                         variant="filled"
                         label="Select Sub Groups"
                         error={!!touched.subGroups && !!errors.subGroups}
                         helperText={touched.subGroups && errors.subGroups}
                         InputLabelProps={{
                         style: { fontSize: "1rem" },
                         focused: false,
                         }}
                         FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
                     />
                     )}
                     renderTags={(tagValue, getTagProps) =>
                     tagValue.map((option, index) => (
                         <Chip
                         key={option}
                         label={option}
                         {...getTagProps({ index })}
                         />
                     ))
                     }
                 />
  
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
                  onClick={() => navigate(findPathByLabel(adminRoutes, "Students"))} // navigating back to the previous page
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
  
  export default StudentUpdateForm;