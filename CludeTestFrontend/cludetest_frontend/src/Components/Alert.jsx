import React,{useEffect} from 'react';
import PropTypes from 'prop-types';
import "./css/Alert.css"

const Alert = ({ type, message }) => {
  
  if (!message) return null; 

  const alertClass = type === 'success' ? 'alert-success' : 'alert-danger';

  return <div className={`alert ${alertClass} my-2 alert-custom`}>{message}</div>;
};

Alert.propTypes = {
  type: PropTypes.oneOf(['success', 'error']),
  message: PropTypes.string,
};

export default Alert;
