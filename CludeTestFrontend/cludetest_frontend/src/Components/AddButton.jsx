import React, { useState } from 'react';
import AddProfessionalModal from '../Components/AddProfissionalModal';
import "./css/AddButton.css";


const AddButton = ({ onProfessionalAdded , onSetAlert  }) => {

  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleModalClose = (reload, message, type) => {
    
    setIsModalOpen(false);
    if (onSetAlert) onSetAlert(message, type);

    if (reload && onProfessionalAdded) {
      onProfessionalAdded(); // Notifica a tabela para recarregar
    }
  };

  return (
    <div>
      <button className="btn btn-success addbtn-custom" onClick={() => setIsModalOpen(true)}>
        Adicionar
      </button>
      {isModalOpen && (
        <AddProfessionalModal onClose={handleModalClose} />
      )}
    </div>
  );
};

export default AddButton;