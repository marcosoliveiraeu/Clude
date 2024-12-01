import React, { useState ,useRef} from 'react';
import Header from '../Components/Header';
import FilterButton from '../Components/FilterButton';
import AddButton from '../Components/AddButton';
import Alert from '../Components/Alert';
import ProfissionaisTable from "../Components/ProfissionaisTable";
import './css/Home.css';


const Home = () => {
  const [filter, setFilter] = useState(null);
  const [alertMessage, setAlertMessage] = useState('');
  const [alertType, setAlertType] = useState('error');
  const tableRef = useRef(null);
  
  const handleFilterChange = (selectedFilter) => {
    setFilter(selectedFilter);
    console.log('Filtro selecionado:', selectedFilter);

  };

  const handleValidation = (hasEspecialidades) => {
    if (!hasEspecialidades) {
      setAlertMessage('Houve um problema ao carregar as especialidades.');
      setAlertType('error');
    } else {
      setAlertMessage('');
      setAlertType('success');
    }
  };


  //mostra a mensagem de sucesso/erro no Alert e fecha depois de 5 segundos
  const handleSetAlert = (message, type) => {
    setAlertMessage(message);
    setAlertType(type);
    setTimeout(() => setAlertMessage(''), 5000); 
  };

  //atualiza a tabela quando incluir um profissional
  const handleProfessionalAdded = () => {
    if (tableRef.current) {
      tableRef.current.refreshTable(); 
    }
  };

  return (
    <div className="home">
      <Header />
      <div className="container my-4">
        <div className="row gy-3 mb-3 align-items-center">
          <div className="col-12 col-md-4 d-flex justify-content-start">
            <FilterButton onFilterChange={handleFilterChange} onValidation={handleValidation} />
          </div>
          <div className="col-12 col-md-4 d-flex justify-content-center align-items-center">
            <Alert type={alertType} message={alertMessage} />
          </div>
          <div className="col-12 col-md-4 d-flex  justify-content-end">
            <AddButton onProfessionalAdded={handleProfessionalAdded} onSetAlert={handleSetAlert} />
          </div>
        </div>
        <div className="row">
          <div className="col-12">
            <ProfissionaisTable filter={filter} ref={tableRef} alertCallback={handleSetAlert} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default Home;
